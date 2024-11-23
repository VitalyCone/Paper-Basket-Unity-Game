using UnityEngine;

public class OutsideTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Ball(Clone)")
            ScoreManager.Manager.ChangeLives(false,false);

        Destroy(collider.gameObject);
    }
}
