using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject[] chairModels;
    private int currentIndex = 0;

    public void SwitchModel()
    {
        // Nonaktifkan semua model
        foreach (GameObject model in chairModels)
        {
            model.SetActive(false);
        }

        // Aktifkan model selanjutnya
        currentIndex = (currentIndex + 1) % chairModels.Length;
        chairModels[currentIndex].SetActive(true);
    }
}