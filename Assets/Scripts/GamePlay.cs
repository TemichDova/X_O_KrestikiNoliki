using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    //______public_______//
    public GameObject _X;
    public GameObject _O;
    public GameObject button;
    public bool SetChessMove { set { chessMove = value; } }
    public int GameMode { get; set; }



    [SerializeField]
    List<Vector2> _posX = new List<Vector2>();
    [SerializeField]
    List<Vector2> _posO = new List<Vector2>();


    [SerializeField]
    List<Vector2> clearPos = new List<Vector2>();

    public List<Transform> clearPointTransform = new List<Transform>();
   
    [SerializeField]
    private bool chessMove;


    
    

    // Use this for initialization
    void Start() {

    }
    // PC ходит !chessMove т.е. O


    // Update is called once per frame
    public void Step(Transform _pos)
    {
        // Player Vs Player
        if(GameMode == 1)
        {
            chessMove = !chessMove;
            if (chessMove)
            {
                Debug.Log("Ход игрока 1"); //Enter X
            }
            else
            {
                Debug.Log("Ход игрока 2"); //Enter 0
            }

            if (_pos.GetChild(1).gameObject.activeSelf == false && _pos.GetChild(0).gameObject.activeSelf == false)
            {
                if (chessMove)
                {
                    //Enter X
                    _pos.GetChild(1).gameObject.SetActive(true);

                    //условие победы
                    CheckWin(new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)));
                    _posX.Add(new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)));
                    

                    //удаление пустой клетки
                    int i = 0;
                    foreach (Vector2 n in clearPos)
                    {
                        //ласт щелчок
                        Vector2 _pospoint = new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y));
                        if (_pospoint == n)
                        {
                            clearPointTransform.RemoveAt(i);
                            clearPos.RemoveAt(i);
                            return;
                        }
                        i++;
                    }

                }
                else
                {
                    //Enter O
                    _pos.GetChild(0).gameObject.SetActive(true);
                    CheckWin(new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)));
                    _posO.Add(new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)));
                    
                    int i = 0;
                    foreach (Vector2 n in clearPos)
                    {
                        if (new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)) == n)
                        {
                            clearPointTransform.RemoveAt(i);
                            clearPos.RemoveAt(i);
                           
                            return;
                        }
                        i++;
                    }
                }
            }

        }

        if(GameMode == 0)
        {
            
            if (chessMove)
            {
                Debug.Log("Ход игрока 1"); //Enter X
            }
            else
            {
                Debug.Log("Ход игрока 2"); //Enter 0
            }

            if (_pos.GetChild(1).gameObject.activeSelf == false && _pos.GetChild(0).gameObject.activeSelf == false)
            {
                if (chessMove)
                {
                    //Enter X
                    _pos.GetChild(0).gameObject.SetActive(true);

                    //условие победы
                    CheckWin(new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)));
                    _posX.Add(new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y)));
                  
                    

                    //удаление пустой клетки
                    int i = 0;
                    foreach (Vector2 n in clearPos)
                    {
                        //ласт щелчок
                        Vector2 _pospoint = new Vector2(Mathf.Round(_pos.position.x), Mathf.Round(_pos.position.y));
                        if (_pospoint == n)
                        {
                            clearPointTransform.RemoveAt(i);
                            clearPos.RemoveAt(i);

                            /*
                            Vector2[] _X = _posX.ToArray();

                            AI(_X, 1);*/
                          
                            chessMove = !chessMove;
                            return;
                        }
                        i++;
                    }

                }
                else
                {
                    //Enter O
                   
                }
            }
        }


    }

    public void SterBotAI()
    {
        //PLayer vs PC
        if (GameMode == 0 && !chessMove)
        {

            Vector2[] _X = _posX.ToArray();
            AI(_X, 1);
            chessMove = !chessMove;
        }

        //PC vs PC
        if (GameMode == 2)
        {
            chessMove = !chessMove;
            if (chessMove)
            {
                //сканирование пративника
                Vector2[] _X = _posX.ToArray();
                AI(_X, 0);
            }
            else
            {
                //сканирование пративника
                Vector2[] _O = _posO.ToArray();
                AI(_O, 1);
            }
        }
    }

    void CheckWin(Vector2 _lastPos)
    {
        if (chessMove)
        {
            Vector2[] _X = _posX.ToArray();
            if (CheckGameWin(_X, _lastPos)) GameMode = -1;
           Debug.Log(CheckGameWin(_X, _lastPos));
            
        }
        else
        {
            Vector2[] _O = _posO.ToArray();
            if(CheckGameWin(_O, _lastPos)) GameMode = -1;
           Debug.Log(CheckGameWin(_O, _lastPos));
        }
    }

    bool CheckGameWin(Vector2[] _pos, Vector2 _lastPos)
    {

        bool win = false;
        int rangeX = 1;
        int rangeY = 1;

        for (int i = 0; i < _pos.Length; i++)
        {

            if (_lastPos.x == _pos[i].x)
            {
                rangeX++;

            }
            if (_lastPos.y == _pos[i].y)
            {
                rangeY++;
            }


        }

        win = (DiagonalChek(_pos, _lastPos) == _SizeMap || rangeX == _SizeMap || rangeY == _SizeMap) ? true : false;
        return win;
    }

    //Проверка по диогонали
    int DiagonalChek(Vector2[] _pos, Vector2 _lastPos)
    {
        int range = 0;
        int rangeLeft = 0; // сверху вниз
        int rangeRight = 0; // снизу верх



        for (int n = 0, radius = _SizeMap / 2, x = 1, y = 1; n < radius; n++, x++, y++)
        {
            for (int i = 0; i < _pos.Length; i++)
            {
                if (_pos[i] == new Vector2(0, 0) && n == 0)
                {

                    rangeRight++;
                    rangeLeft++;
                }
                if (_pos[i] == new Vector2(-x, -y))
                {

                    rangeRight++;
                }
                if (_pos[i] == new Vector2(x, y))
                {

                    rangeRight++;
                }

                if (_pos[i] == new Vector2(-x, y))
                {

                    rangeLeft++;
                }
                if (_pos[i] == new Vector2(x, -y))
                {

                    rangeLeft++;
                }

            }
            if (_lastPos == new Vector2(0, 0) && n == 0)
            {

                rangeLeft++;
                rangeRight++;
            }
            if (_lastPos == new Vector2(-x, -y))
            {

                rangeRight++;
            }
            if (_lastPos == new Vector2(x, y))
            {

                rangeRight++;
            }

            if (_lastPos == new Vector2(-x, y))
            {

                rangeLeft++;
            }
            if (_lastPos == new Vector2(x, -y))
            {

                rangeLeft++;
            }
        }
        return range = (rangeLeft > rangeRight) ? rangeLeft : rangeRight;
    }

    
   

    //отвчеает за пустые клетки
    public void SetClearPosActive(Vector2[] _posGrid)
    {
        for (int i = 0; i < _posGrid.Length; i++)
        {
            clearPos.Add(new Vector2(_posGrid[i].x/60, _posGrid[i].y/60));
        }

    }

    public int _SizeMap { get;  set;}

    public void ClearPos()
    {
        clearPointTransform.Clear();
        clearPos.Clear();
        _posX.Clear();
        _posO.Clear();
    }

   
    // Бот____________________________________________//
    void AI(Vector2[] _posYou, int indexPlayer)
    {
      //_posYou координаты противника
      
        
        // chessMove = false;
        
        
        if (!CheckYou(_posYou, indexPlayer))
        {
            if(indexPlayer == 1)
            {
                Vector2[] _O = _posO.ToArray();
                if (_O.Length == 0)
                {
                    FirstPOint(indexPlayer);
                }
                else
                {
                    CheckMe(_O, indexPlayer);
                }
            }
            else
            {
                Vector2[] _X = _posX.ToArray();
                if (_X.Length == 0)
                {
                    FirstPOint(indexPlayer);
                }
                else
                {
                    CheckMe(_X, indexPlayer);
                }
            }
            
          
        }

       
    }

    
    //сканирование противника p.s. должен препяствовать игроку, иногда не срабатывает
    bool CheckYou(Vector2[] _posYou, int indexPlayer)
    {
        bool _checkYou = false;
        //сканирование по вертикале
        for(int i = 0, range = 0, x = -_SizeMap/2; i<_SizeMap  ;x++ , i++, range=0)
        {
            foreach (Vector2 n in _posYou)
            {
                if (n.x == x)
                {
                    range++;
                    _checkYou = (range == _SizeMap  - 1) ? true: false ;
                    if (_checkYou)
                    {
                        
                        ClickAI(x,0,true, indexPlayer);
                        return _checkYou;
                    }
                }
            }
        }

        //сканирование по горизонтале
        for (int i = 0, range = 0, y = -_SizeMap / 2; i < _SizeMap; y++, i++, range = 0)
        {
            foreach (Vector2 n in _posYou)
            {
                if (n.y == y)
                {
                    range++;
                    _checkYou = (range == _SizeMap - 1) ? true : false;
                    if (_checkYou)
                    {

                        ClickAI(0, y, false, indexPlayer);

                        return _checkYou;
                    }
                }
            }
        }

        return _checkYou;
    }

    // Сканирование себя KappaPride
    void CheckMe(Vector2[] _posMEMEs, int indexPlayer)
    {
        int[] massivMaxX = new int[_SizeMap];
        int[] massivMaxY = new int[_SizeMap];

        int maxX=0;
        int maxY=0;
        //сканирование по вертикале
        for (int i = 0, range = 0, x = -_SizeMap / 2; i < _SizeMap; x++, i++, range = 0)
        {
            foreach (Vector2 n in _posMEMEs)
            {
                if (n.x == x)
                {
                    range++;

                    
                    //ClickAI(x, 0, true, indexPlayer);
                    
                }

            }
            massivMaxX[i] = range;
        }

        //сканирование по горизонтале
        for (int i = 0, range = 0, y = -_SizeMap / 2; i < _SizeMap; y++, i++, range = 0)
        {
            foreach (Vector2 n in _posMEMEs)
            {
                if (n.y == y)
                {
                    range++;
                       
                   //ClickAI(y, 0, false, indexPlayer);
                }
            }
            massivMaxY[i] = range;
        }

        for(int i=0; i < _SizeMap; i++)
        {
            if (maxX < massivMaxX[i]) maxX = massivMaxX[i];
            if (maxY < massivMaxY[i]) maxY = massivMaxY[i];
        }

        if (maxX<maxY)
        {
            ClickAI(0, maxY, false,indexPlayer);
        }
        else
        {
            ClickAI(maxX, 0, true, indexPlayer);
        }
    }

    // Ставит первую точку
    void FirstPOint(int indexPlayer)
    {
       if(indexPlayer == 1)
        {
            int index = Random.Range(0, clearPos.Count);
            clearPointTransform[index].GetChild(indexPlayer).gameObject.SetActive(true);
            CheckWin(new Vector2(Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.y)));
            _posO.Add(new Vector2(Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.y)));
            clearPointTransform.RemoveAt(index);
            clearPos.RemoveAt(index);
        }
        else
        {
            int index = Random.Range(0, clearPos.Count);
            clearPointTransform[index].GetChild(indexPlayer).gameObject.SetActive(true);
            CheckWin(new Vector2(Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.y)));
            _posX.Add(new Vector2(Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[index].GetChild(indexPlayer).position.y)));
            clearPointTransform.RemoveAt(index);
            clearPos.RemoveAt(index);
        }
       

        


    }

    void ClickAI(int pos_X, int pos_Y, bool beep, int indexPlayer)
    {
        
        for (int i = 0; i<clearPos.Count; i++)
        {
            if(indexPlayer == 1)
            {
                if (clearPos[i].x == pos_X && beep)
                {
                    clearPointTransform[i].GetChild(indexPlayer).gameObject.SetActive(true);
                    CheckWin(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                    _posO.Add(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                    clearPointTransform.RemoveAt(i);
                    clearPos.RemoveAt(i);
                    return;
                }
                else
                {
                    if (clearPos[i].y == pos_Y && !beep)
                    {
                        clearPointTransform[i].GetChild(indexPlayer).gameObject.SetActive(true);
                        CheckWin(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                        _posO.Add(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                        clearPointTransform.RemoveAt(i);
                        clearPos.RemoveAt(i);
                        return;

                    } //может из-за этого не корректно ставит подлямки игроку p.s. да из-за этого препяствует
                    else
                    {
                        FirstPOint(indexPlayer);
                        return;
                    }
                }
            }
            else
            {
                if (clearPos[i].x == pos_X && beep)
                {
                    clearPointTransform[i].GetChild(indexPlayer).gameObject.SetActive(true);
                    CheckWin(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                    _posX.Add(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                    clearPointTransform.RemoveAt(i);
                    clearPos.RemoveAt(i);
                    return;
                }
                else
                {
                    if (clearPos[i].y == pos_Y && !beep)
                    {
                        clearPointTransform[i].GetChild(indexPlayer).gameObject.SetActive(true);
                        CheckWin(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                        _posX.Add(new Vector2(Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.x), Mathf.Round(clearPointTransform[i].GetChild(indexPlayer).position.y)));
                        clearPointTransform.RemoveAt(i);
                        clearPos.RemoveAt(i);
                        return;

                    } //может из-за этого не корректно ставит подлямки игроку  p.s. да из-за этого препяствует
                    else
                    {
                        FirstPOint(indexPlayer);
                        return;
                    }
                }
            }
            

            
        }


    }
}
