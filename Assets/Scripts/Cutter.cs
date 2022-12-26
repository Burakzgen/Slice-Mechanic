using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] private Transform cutter_1, cutter_2;
    [SerializeField] private float speed;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            cutter_1.transform.position += new Vector3(0, 0, -.1f * Time.deltaTime * speed);
            cutter_2.transform.position += new Vector3(0, 0, -.1f * Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Saw"))
        {
            Cut(other.gameObject.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Saw"))
        {
            cutter_1.transform.position += new Vector3(Random.Range(0.08f, 0.45f), 0, 1f);
            cutter_2.transform.position += new Vector3(Random.Range(-0.1f, -0.45f), 0, 1f);
            speed = -speed;
        }
    }
    public void Cut(Transform cutter)
    {
        if (cutter.transform.position.x < 0)
        {
            float y = transform.localScale.y;
            y -= transform.position.x;
            float distance = y + cutter.position.x;
            if (distance / 2 > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - distance / 2, transform.localScale.z);
                transform.position = new Vector3(transform.position.x + distance / 2, transform.position.y, transform.position.z);

                GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                newObject.transform.localScale = new Vector3(transform.localScale.x, distance / 2, transform.localScale.z);
                newObject.transform.rotation = transform.rotation;
                newObject.transform.position = new Vector3(-(y - newObject.transform.localScale.y), transform.position.y, transform.position.z);
                newObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

                newObject.AddComponent<Rigidbody>();

            }
        }
        else
        {
            float y = transform.localScale.y;
            y += transform.position.x;
            float distance = y - cutter.position.x;

            if (distance / 2 > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - distance / 2, transform.localScale.z);
                transform.position = new Vector3(transform.position.x - distance / 2, transform.position.y, transform.position.z);

                GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                newObject.transform.localScale = new Vector3(transform.localScale.x, distance / 2, transform.localScale.z);
                newObject.transform.rotation = transform.rotation;
                newObject.transform.position = new Vector3(y - newObject.transform.localScale.y, transform.position.y, transform.position.z);
                newObject.GetComponent<Renderer>().material.color = Random.ColorHSV();

                newObject.AddComponent<Rigidbody>();
            }
        }
    }
}
