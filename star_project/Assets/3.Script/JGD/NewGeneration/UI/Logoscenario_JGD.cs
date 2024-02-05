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
        //활성화 되지 않은 상태에서도 게임이 계속 진행
        Application.runInBackground = true;
        //해상도?
        int width = Screen.width;
        int height = Screen.height;
        Screen.SetResolution(width, height, true);

        //화면이 꺼지지 않도록 설정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //로딩 애니메이션 시작, 재생 완료시 OnafterProgress실행
        progress.Play(OnafterProgress);
    }

    private void OnafterProgress()
    {
        SceneUtills_JGD.LoadScene(nextScene.ToString());
    }



}
