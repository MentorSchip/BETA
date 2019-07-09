using UnityEngine;
using Mapbox.Unity.Map;

public interface IMapSpawnable
{
    GameObject GameObject();
    void SetPosition(ScholarshipSet scholarship, MapPage gameMap, AbstractMap mapbox);
}
