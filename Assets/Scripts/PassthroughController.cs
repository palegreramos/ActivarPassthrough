using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static OVRPlugin;




public class PassthroughController : MonoBehaviour
{
    public OVRManager ovrManager;
    public OVRPassthroughLayer passthroughLayer;
    public Camera cam;
    public TMP_Text texto;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ovrManager == null)
        {
            ovrManager = Object.FindFirstObjectByType<OVRManager>();
        }
        if (passthroughLayer == null)
        {
            passthroughLayer = Object.FindFirstObjectByType<OVRPassthroughLayer>();
        }
        if (texto == null)
        {
            texto = Object.FindFirstObjectByType<TMP_Text>();
        }
    }

    public void Mover(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2 moveInput = ctx.ReadValue<Vector2>();
            if (DetectHand(ctx) == "LEFT")
            {
                texto.text = $"Move Input: {moveInput} from left";
            }
            else if (DetectHand(ctx) == "RIGHT")
            {
                texto.text = $"Move from right";
            }
           
        }
      
    }
    public void Grip(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            texto.text = $"Grip Button Pressed {DetectHand(ctx)}";
            TogglePassthrough();
        }
    }
    public void Aumentar(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            texto.text = $"Primary Button Pressed {DetectHand(ctx)}";
            texto.fontSize++;
        }
    }

    public void Disminuir(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            texto.text = $"Primary Button Pressed {DetectHand(ctx)}";
            texto.fontSize--;
        }
    }

    public void Trigger(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            texto.text = $"Trigger Button Pressed {DetectHand(ctx)}";
            TogglePassthrough();
        }
    }
    public void Secondary(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            texto.text = $"Secondary Button Pressed {DetectHand(ctx)}";
          
        }
    }
    // Detecta si es mano izquierda o derecha
    private string DetectHand(InputAction.CallbackContext ctx)
    {
        XRController controller = (XRController)ctx.control.device;

        if (controller == XRController.leftHand)
            return "LEFT";

        if (controller == XRController.rightHand)
            return "RIGHT";

        return "UNKNOWN";
    }

    public void TogglePassthrough()
    {
        //if (ovrManager != null)
        //{
        //    ovrManager.isInsightPassthroughEnabled = !ovrManager.isInsightPassthroughEnabled;
        //}
        if (passthroughLayer != null)
        {
            passthroughLayer.hidden = !passthroughLayer.hidden;
        }
        ToggleBackground();
    }
    public void ToggleBackground()
    {
        cam.clearFlags = cam.clearFlags == CameraClearFlags.SolidColor
            ? CameraClearFlags.Skybox
            : CameraClearFlags.SolidColor;
    }
}
