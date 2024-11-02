using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public Transform playerTrans;//������ק��ֵ����persistent��
    public SceneLoadEventSO LoadEventSO;
    public GameSceneSO FirstLoadScene;
    private GameSceneSO sceneload;
    private Vector3 poToGo;
    private bool faceScreen;//������ʱ��һ�£�������һ��������������
    private GameSceneSO currentGameScene;
    public float fadeDuration;//����ȴ�ʱ��
    private void Awake()
    {//������ȫ����Ҳ�з����жϽ���֮���ʲô����
        //Addressables.LoadSceneAsync(FirstLoadScene.sceneReference, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        currentGameScene = FirstLoadScene;//��ֵ֮���ǲ�һ������������������
        currentGameScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);    
    }
    private void OnEnable()
    {
        LoadEventSO.loadRequestEvent += OnLoadRequestEvent;
    }
    private void OnDisable()
    {
        LoadEventSO.loadRequestEvent -= OnLoadRequestEvent;
    }

    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector3 PositionToGo, bool faceScreen)
    {
        sceneload = locationToLoad;
        poToGo = PositionToGo;
        this.faceScreen = faceScreen;//������face.��֪ʶ��
        //Debug.Log(sceneload.sceneReference.SubObjectName);
        StartCoroutine(UnLoadPreviousScene());

    }
    private IEnumerator UnLoadPreviousScene()//ж����һ������
    {
        if(faceScreen)
        {
            //�Ƚ��뽥��ʵ����
        }
        yield return new WaitForSeconds(fadeDuration);
        if (currentGameScene != null)
        {
            currentGameScene.sceneReference.UnLoadScene();//unity�Դ�ж�ط���������������
        }
        LoadNewScene();
    }
    private void LoadNewScene()
    {
      var loadingOption=  sceneload.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);//������ʱ��������var��ִ���¼�����sceneInstance������
        loadingOption.Completed += OnLoadCompleted;//��ǰ��Ĵ洢ֵ������complete
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)//�������غ�֮��ִ���������
    {
        currentGameScene = sceneload;
        playerTrans.position = poToGo;
        if(faceScreen)//��fade���face��
        {
            //����
        }

    }
}
