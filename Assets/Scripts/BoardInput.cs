using UnityEngine;
using UnityEngine.InputSystem;

public class BoardInput : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private PlayerMover player;

    private BoardInputActions input;
    private bool moveHeld;

    private void Awake()
    {
        input = new BoardInputActions();
    }

    private void OnEnable()
    {
        if(input == null) input = new BoardInputActions();
        input.Board.Enable();
        input.Board.Click.performed += OnClick;
    }
    private void Update()
    {
        HandleMoveInput();
    }

    private void OnDisable()
    {
        input.Board.Click.performed -= OnClick;

        input.Board.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if(player.IsMoving) return;
        if (player.RemainingMove <= 0) return; //移動回数ない時絶対にやらせｎ

        Vector2 mousePos =Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            CellNode clickedCell =
                hit.collider.GetComponent<CellNode>();

            if(clickedCell == null) return;

            bool canMove =player.currentNode.nextNodes.Contains(clickedCell);

            if(!canMove) return;

            player.MoveTo(clickedCell);
        }
    }
    private void HandleMoveInput()
    {
        if(player.IsMoving) return;
        if (player.RemainingMove <= 0) return; //移動回数ない時絶対にやらせｎ　　２
        
        Vector2 inputDir =input.Board.Move.ReadValue<Vector2>();
        
        // スティック・キーが離されたら再入力可能
        if(inputDir.magnitude < 0.5f)
        {
            moveHeld = false;
            return;
        }
        // 押しっぱなし防止
        if(moveHeld) return;
        
        moveHeld = true;
        
        Vector3 desired =new Vector3(inputDir.x,
            0f,
            inputDir.y
        ).normalized;
        CellNode best = null;
        float bestDot = 0.5f;  // 1.0  完全一致 ,0.7  45°くらい,0.5  60°くらい,0.0  真横,-1.0 真後ろ
        foreach (CellNode node in player.currentNode.nextNodes)
        {
            Vector3 dir =
            (node.transform.position- player.currentNode.transform.position);
            
            dir.y = 0f;
            dir.Normalize();
            float dot = Vector3.Dot(desired, dir);
            
            if(dot > bestDot)
            {
                bestDot = dot;
                best = node;
            }
        }

        if(best != null){
            Debug.Log($"Move to {best.name}"); //確認
            player.MoveTo(best);
        }
        
    }
}