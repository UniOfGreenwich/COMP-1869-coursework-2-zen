using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode] // This allows the script to run without hitting "Play"
public class GrassSpawn : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Drag your Terrain object here")]
    public Terrain targetTerrain;

    [Tooltip("Drag your SpeedTree Grass Prefab here")]
    public GameObject grassPrefab;

    [Tooltip("Which Terrain Layer (Texture) index should the grass grow on? (0 is the first layer, 1 is the second...)")]
    public int textureLayerIndex = 0;

    [Tooltip("How strong must the texture be for grass to grow? (0.5 means the texture must be at least 50% visible)")]
    [Range(0f, 1f)]
    public float textureThreshold = 0.5f;

    [Header("Spawning Stats")]
    [Tooltip("Total amount of grass patches you want to attempt to spawn")]
    public int spawnCount = 10000;

    [Tooltip("Minimum size of the grass")]
    public float minScale = 0.8f;
    [Tooltip("Maximum size of the grass")]
    public float maxScale = 1.2f;

    [Header("Actions")]
    [Tooltip("Check this box to clear ONLY this specific grass prefab before spawning new ones.")]
    public bool clearExistingGrassOfType = true;

    // This button appears in the Inspector context menu
    [ContextMenu("Spawn Grass")]
    public void SpawnGrass()
    {
        if (targetTerrain == null || grassPrefab == null)
        {
            Debug.LogError("GrassSpawn: Please assign a Terrain and a Grass Prefab.");
            return;
        }

        TerrainData terrainData = targetTerrain.terrainData;

        // 1. Register the Prefab in the Terrain Tree Prototypes if not already there
        int prototypeIndex = GetOrAddPrototypeIndex(terrainData);

        // 2. Get existing trees so we don't delete other vegetation
        List<TreeInstance> currentTrees = new List<TreeInstance>(terrainData.treeInstances);

        // 3. Remove existing grass of this type if requested
        if (clearExistingGrassOfType)
        {
            currentTrees.RemoveAll(x => x.prototypeIndex == prototypeIndex);
        }

        // 4. Prepare Splatmap (Texture data)
        int alphaWidth = terrainData.alphamapWidth;
        int alphaHeight = terrainData.alphamapHeight;
        // Get all texture weights for the whole terrain
        float[,,] splatmapData = terrainData.GetAlphamaps(0, 0, alphaWidth, alphaHeight);

        int spawnedCount = 0;

        // 5. Spawn Loop
        for (int i = 0; i < spawnCount; i++)
        {
            // Pick a random spot (0.0 to 1.0)
            float normalizedX = Random.value;
            float normalizedZ = Random.value;

            // Convert that spot to the splatmap coordinate
            int mapX = (int)(normalizedX * (alphaWidth - 1));
            int mapZ = (int)(normalizedZ * (alphaHeight - 1));

            // Check if the assigned texture layer is dominant here
            // Safety check to ensure we don't look for a layer that doesn't exist
            if (textureLayerIndex < splatmapData.GetLength(2))
            {
                float textureStrength = splatmapData[mapZ, mapX, textureLayerIndex];

                // If texture is strong enough, spawn grass
                if (textureStrength >= textureThreshold)
                {
                    TreeInstance instance = new TreeInstance();
                    instance.position = new Vector3(normalizedX, 0, normalizedZ); // Y is automatically calculated by terrain
                    instance.prototypeIndex = prototypeIndex;

                    // Randomize rotation and scale
                    instance.rotation = Random.Range(0f, 360f); // Random rotation
                    float scale = Random.Range(minScale, maxScale);
                    instance.widthScale = scale;
                    instance.heightScale = scale;

                    // Add color variation (optional, keeps it white/original mostly)
                    instance.color = Color.white;
                    instance.lightmapColor = Color.white;

                    currentTrees.Add(instance);
                    spawnedCount++;
                }
            }
        }

        // 6. Apply back to terrain
        terrainData.treeInstances = currentTrees.ToArray();

        // Force terrain to update
        targetTerrain.Flush();
        Debug.Log($"GrassSpawn: Successfully spawned {spawnedCount} grass patches on Layer {textureLayerIndex}.");
    }

    [ContextMenu("Clear All Grass Of This Type")]
    public void ClearOnlyThisGrass()
    {
        if (targetTerrain == null || grassPrefab == null) return;

        TerrainData terrainData = targetTerrain.terrainData;
        int prototypeIndex = GetOrAddPrototypeIndex(terrainData);

        List<TreeInstance> currentTrees = new List<TreeInstance>(terrainData.treeInstances);
        int removedCount = currentTrees.RemoveAll(x => x.prototypeIndex == prototypeIndex);

        terrainData.treeInstances = currentTrees.ToArray();
        targetTerrain.Flush();

        Debug.Log($"Removed {removedCount} instances of {grassPrefab.name}");
    }

    // Helper function to handle the internal Terrain ID system
    private int GetOrAddPrototypeIndex(TerrainData data)
    {
        TreePrototype[] prototypes = data.treePrototypes;

        // Check if our grass is already in the list
        for (int i = 0; i < prototypes.Length; i++)
        {
            if (prototypes[i].prefab == grassPrefab)
            {
                return i;
            }
        }

        // If not, add it
        TreePrototype[] newPrototypes = new TreePrototype[prototypes.Length + 1];
        System.Array.Copy(prototypes, newPrototypes, prototypes.Length);

        TreePrototype newProto = new TreePrototype();
        newProto.prefab = grassPrefab;
        newPrototypes[newPrototypes.Length - 1] = newProto;

        data.treePrototypes = newPrototypes;
        data.RefreshPrototypes(); // Critical for Unity 6/HDRP to recognize the change

        return newPrototypes.Length - 1;
    }
}