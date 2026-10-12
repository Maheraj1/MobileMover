using UnityEngine;
[CreateAssetMenu(fileName = "new MobileApp", menuName = "ScriptableObjects/MobileApp")]
public class MobileApp_SO : ScriptableObject {
    [Tooltip("Must be unique (Pleaseeeeee)")]
    public int Id = 0;
    public string MobileAppName;
    public Transform MobileAppUI;
}
