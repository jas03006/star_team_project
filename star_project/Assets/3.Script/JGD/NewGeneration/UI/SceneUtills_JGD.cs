using UnityEngine.SceneManagement;

public enum SceneNames
{
    Logo = 0,
    Login,
    Lobby,
}
public static class SceneUtills_JGD               //씬이동 
{
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    public static void LoadScene(string sceneName = "")
    {
        if (sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public static void Loadscene(SceneNames sceneNames)
    {
        //SceneName 열거형으로 매개변수를 받아온 경우 ToString()처리
        SceneManager.LoadScene(sceneNames.ToString());
    }
}