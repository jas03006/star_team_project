using UnityEngine;

public class Logoscenario_JGD : MonoBehaviour
{
    [SerializeField] private Progress_JGD progress;
    [SerializeField] private SceneNames nextScene;
    private void Awake()
    {
        SystemSetup();
    }
    private void SystemSetup()
    {
        //Ȱ��ȭ ���� ���� ���¿����� ������ ��� ����
        Application.runInBackground = true;
        //�ػ�?
        int width = Screen.width;
        int height = Screen.height;
        Screen.SetResolution(width, height, true);

        //ȭ���� ������ �ʵ��� ����
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //�ε� �ִϸ��̼� ����, ��� �Ϸ�� OnafterProgress����
        progress.Play(OnafterProgress);
    }

    private void OnafterProgress()
    {
        SceneUtills_JGD.LoadScene(nextScene.ToString());
    }



}
