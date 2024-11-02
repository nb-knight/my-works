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
    public Transform playerTrans;//可以拖拽赋值，都persistent的
    public SceneLoadEventSO LoadEventSO;
    public GameSceneSO FirstLoadScene;
    private GameSceneSO sceneload;
    private Vector3 poToGo;
    private bool faceScreen;//这仨临时存一下，先让上一个场景淡出再用
    private GameSceneSO currentGameScene;
    public float fadeDuration;//渐入等待时间
    private void Awake()
    {//进程完全结束也有方法判断结束之后干什么？？
        //Addressables.LoadSceneAsync(FirstLoadScene.sceneReference, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        currentGameScene = FirstLoadScene;//赋值之后是不一样的两个场景？？？
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
        this.faceScreen = faceScreen;//可以用face.涨知识了
        //Debug.Log(sceneload.sceneReference.SubObjectName);
        StartCoroutine(UnLoadPreviousScene());

    }
    private IEnumerator UnLoadPreviousScene()//卸载上一个场景
    {
        if(faceScreen)
        {
            //等渐入渐出实行完
        }
        yield return new WaitForSeconds(fadeDuration);
        if (currentGameScene != null)
        {
            currentGameScene.sceneReference.UnLoadScene();//unity自带卸载方法，不用再声明
        }
        LoadNewScene();
    }
    private void LoadNewScene()
    {
      var loadingOption=  sceneload.sceneReference.LoadSceneAsync(LoadSceneMode.Additive,true);//创建临时变量储存var，执行事件返回sceneInstance的类型
        loadingOption.Completed += OnLoadCompleted;//有前面的存储值可以用complete
    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)//场景加载好之后执行这个函数
    {
        currentGameScene = sceneload;
        playerTrans.position = poToGo;
        if(faceScreen)//靠fade打成face了
        {
            //渐出
        }

    }
}
