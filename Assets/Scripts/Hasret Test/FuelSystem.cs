using UnityEngine;
using UnityEngine.UI; // Eğer UI kullanıyorsanız

public class FuelSystem : MonoBehaviour
{
    public float maxFuel = 100f; // Maksimum yakıt miktarı
    public float currentFuel; // Mevcut yakıt miktarı
    public float fuelConsumptionRate = 10f; // Yakıt tüketim hızı (birim/saniye)
    public Text fuelText; // Yakıt miktarını gösteren UI metin bileşeni

    private Rigidbody rb;

    void Start()
    {
        currentFuel = maxFuel; // Başlangıçta mevcut yakıtı maksimum yakıt miktarına ayarla
        rb = GetComponent<Rigidbody>(); // Arabanın Rigidbody bileşenini al
        UpdateFuelUI();
    }

    void Update()
    {
        if (currentFuel > 0 && rb.velocity != Vector3.zero) // Eğer yakıt varsa ve araç hareket ediyorsa
        {
            ConsumeFuel(); // Yakıt tüket
        }
        else if (currentFuel <= 0)
        {
            rb.velocity = Vector3.zero; // Yakıt bitince aracı durdur
        }

        UpdateFuelUI(); // Yakıt miktarını UI'da güncelle
    }

    void ConsumeFuel()
    {
        currentFuel -= (fuelConsumptionRate+(0.5f*rb.velocity.magnitude)) * Time.deltaTime; // Yakıtı tüket
        Debug.Log("fuelconsumptionrate: "+fuelConsumptionRate);
        Debug.Log("if: " + fuelConsumptionRate * Time.deltaTime);
        Debug.Log("total harcanan: " + (fuelConsumptionRate + (0.2f * rb.velocity.magnitude)) * Time.deltaTime);
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel); // Yakıt miktarını 0 ile maksimum arasında sınırlı tut
        
            CoreUISignals.Instance.onGameFuelTextUpdate?.Invoke(currentFuel);
        
    }

    void UpdateFuelUI()
    {
        if (fuelText != null)
        {
            fuelText.text = "Fuel: " + (int)currentFuel;//.ToString("F2"); // Yakıt miktarını UI'da göster
        }
    }

    public void Refuel(float amount)
    {
        currentFuel += amount; // Belirtilen miktarda yakıt ekle
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel); // Yakıt miktarını 0 ile maksimum arasında sınırlı tut
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("FuelTank"))
        {
            Refuel(300f); // Yakıt ekle (50 birim)
            Destroy(collision.gameObject);
        }
    }

}
