using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{


    public GameObject Player;
    public Camera cam;
    private int frameDelay = 2;
    public string RestartScene = "RestartTseen";



    private bool IsVisible(Camera c, GameObject Player)
    {
        if (Player == null) return false;

        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = Player.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {

        if (frameDelay > 0)
        {
            frameDelay--;
            return;
        }

        var targetTender = Player.GetComponent<Renderer>();

        if (Player != null && !IsVisible(cam, Player))
        {
            Destroy(Player);
            Invoke("Restart", 0.2f);

        }
    }

    void Restart()
    {
        SceneManager.LoadScene(RestartScene);
    }
}
