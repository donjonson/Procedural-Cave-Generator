﻿using UnityEngine;
using CaveGeneration.MeshGeneration;

using Map = CaveGeneration.MapGeneration.Map;

namespace CaveGeneration
{
    /// <summary>
    /// A 3D cave generator with an isometric camera in mind. Generates mesh colliders for the walkable floors as well as
    /// the walls around them, but not the ceilings. 
    /// </summary>
    public sealed class CaveGeneratorIsometric : CaveGenerator
    {
        [SerializeField] Material ceilingMaterial;
        [SerializeField] Material wallMaterial;
        [SerializeField] Material floorMaterial;

        override protected MeshGenerator PrepareMeshGenerator(Map map)
        {
            MeshGenerator meshGenerator = new MeshGenerator(Map.maxSubmapSize, map.Index.ToString());
            meshGenerator.GenerateIsometric(MapConverter.ToWallGrid(map), floorHeightMap, mainHeightMap);
            return meshGenerator;
        }

        protected override CaveMeshes CreateMapMeshes(MeshGenerator meshGenerator)
        {
            string index = meshGenerator.Index;
            Transform sector = ObjectFactory.CreateSector(index, Cave.transform).transform;
            
            Mesh wallMesh = ObjectFactory.CreateComponent(meshGenerator.GetWallMesh(), sector, wallMaterial, "Wall", true);
            Mesh floorMesh = ObjectFactory.CreateComponent(meshGenerator.GetFloorMesh(), sector, floorMaterial, "Floor", true);
            Mesh ceilingMesh = ObjectFactory.CreateComponent(meshGenerator.GetCeilingMesh(), sector, ceilingMaterial, "Ceiling", false);

            return new CaveMeshes(ceilingMesh, wallMesh, floorMesh);
        }
    } 
}