using UnityEngine.SceneManagement;

public enum SceneNames
{
    Logo = 0,
    Login,
    Lobby,
}
public static class SceneUtills_JGD               //���̵� 
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
        //SceneName ���������� �Ű������� �޾ƿ� ��� ToString()ó��
        SceneManager.LoadScene(sceneNames.ToString());
    }
}