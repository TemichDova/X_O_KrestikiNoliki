using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour {


    //public______________________//
    public Toggle _3x3;
    public Toggle _5x5;
    public Toggle _plVsPc;
    public Toggle _plVsPl;
    public Toggle _pcVsPc;
    public GameObject _GamePlay;

    //private______________________//
    //размер поля
    private int sizeField;

    // режим игры
    // 0 - player vs PC
    // 1 - player vs player
    // 2 - PC vs PC
    private int gameMode;

    //чей ход
    private bool chessMove;

    //Номер игрока/бота
    private int numberPlayr;


    // Use this for initialization
    void Start () {
        _3x3.isOn = true;
        _5x5.isOn = false;
        _plVsPc.isOn = true;
        _plVsPl.isOn = false;
        _pcVsPc.isOn = false;
    }

    public void SizeFildGame(int _sizeField)
    {
        //

        if (_sizeField == 3 && _3x3.isOn == true )
        {
            _5x5.isOn = false;
            sizeField = _sizeField;
           
        }
       if (_sizeField == 5 && _5x5.isOn == true )
        {
           _3x3.isOn = false;
           sizeField = _sizeField;     

        }


        
    }

    
    public void GameModeGame(int _gameMode)
    {
        
        if (_gameMode == 0 && _plVsPc.isOn == true)
        {
            _plVsPl.isOn = false;
            _pcVsPc.isOn = false;
            gameMode = _gameMode;
        }
        if (_gameMode == 1 && _plVsPl.isOn == true)
        {   
            _plVsPc.isOn = false;
            _pcVsPc.isOn = false;
            gameMode = _gameMode;
        }
        if (_gameMode == 2 && _pcVsPc.isOn == true)
        {
            _plVsPc.isOn = false;
            _plVsPl.isOn = false;
            gameMode = _gameMode;
        }
    }

    public void StartGame()
    {


        SettingGame PlayeGame = new SettingGame(sizeField, gameMode);

       
       
        if (sizeField == 0)
        {
            PlayeGame.DefoltGame();
        }
        
       // PlayeGame.ShowSetting();
       //генерирует сетку
        PlayeGame.GridGeneration();

        _GamePlay.GetComponent<GamePlay>().ClearPos();
        //раставляет префабы
        GenerationGrid(PlayeGame.GetPointGrid);

        //стартует игру
        _GamePlay.GetComponent<GamePlay>()._SizeMap = PlayeGame.GetSizeViled;
        _GamePlay.GetComponent<GamePlay>().SetClearPosActive(PlayeGame.GetPointGrid);
        _GamePlay.GetComponent<GamePlay>().SetChessMove = (Random.Range(0,2)==0)?true:false;
       _GamePlay.GetComponent<GamePlay>().GameMode = gameMode;
  
    }

    public void GenerationGrid(Vector2[] _posPoint)
    {
        for (int i=0;i<_posPoint.Length;i++)
        {
            GameObject prefab = Instantiate(prefabCell,_posPoint[i],Quaternion.identity) as GameObject;
            prefab.transform.SetParent(GameObject.Find("Canvas").transform, false);
            _GamePlay.GetComponent<GamePlay>().clearPointTransform.Add(prefab.transform);

        }
    }   

    public GameObject prefabCell;

}
