using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonManager : MonoBehaviour
{
    #region
    public GameObject model_1;
    public GameObject model_2;
    [SerializeField]private Animator model_1_animator;
    [SerializeField]private Animator model_2_animator;

    private bool isAnimation_1_playing = false;
    private bool isAnimation_2_playing = false;


    public GameObject mainMenu;
    public GameObject transforMenu;
    public GameObject customizeMenu;
    public GameObject changeModelMenu;
    public GameObject animateMenu;
    public float menuSpeed;

    private bool isRotatingLeft = false;
    [SerializeField]private float rotationSpeed;

    private Vector3 lookAt;
    private Vector3 modelScale;
    private Tween rotationModel_1;
    private Tween rotationModel_2;

    [SerializeField] private float addScale;
    [SerializeField] private Vector3 initialScale;

    private Vector3 currentScale;

    public GameObject[] model_1_Parts;
    public Material[] model_1_Materials;

    public GameObject[] model_2_Parts;
    public Material[] model_2_Materials;

    private int currentMaterialIndexModel_1;
    private int currentMaterialIndexModel_2;
    public Material initiaModel_1_lMaterial;
    public Material initiaModel_2_lMaterial;

    public float mainMenuTargetPos;
    #endregion
    // Start is called before the first frame update

    public void Start()
    {
        model_1_animator = model_1.GetComponent<Animator>();
        model_2_animator = model_2.GetComponent<Animator>();

        menuSpeed = menuSpeed * Time.deltaTime;
    }

    public void returnToMenu()
    {
        //return to main menu
        Debug.Log("returnToMenu");
        mainMenu.transform.DOLocalMoveY(0f, menuSpeed).SetEase(Ease.OutBack);
        transforMenu.transform.DOLocalMoveX(-1200, menuSpeed).SetEase(Ease.InOutBack);
        customizeMenu.transform.DOLocalMoveX(1200, menuSpeed).SetEase(Ease.InOutBack);
        animateMenu.transform.DOLocalMoveX(-1200, menuSpeed).SetEase(Ease.InOutBack);
        changeModelMenu.transform.DOLocalMoveX(1200, menuSpeed).SetEase(Ease.InOutBack);

        //reset model rotation
        stopRotate();
    }

    public void restAll()
    {
        //reset model scale
        resetScale();
        //reset material
        resetMaterial();
        //stop animation
        stopAnimation();
    }

    public void transformPlayer()
    {
        //show transform menu
        Debug.Log("transform");
        mainMenu.transform.DOLocalMoveY(mainMenuTargetPos, menuSpeed).SetEase(Ease.InBack);
        transforMenu.transform.DOLocalMoveX(0f, menuSpeed).SetEase(Ease.InOutBack);
    }

    public void transformRotateLeft()
    {
        Debug.Log("rotatePlayer");

        isRotatingLeft = true;
       
        if(isRotatingLeft) 
        {
          rotationModel_1 = model_1.transform.DOLocalRotate (Vector3.up, 5f * rotationSpeed * Time.deltaTime).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
          rotationModel_2 = model_2.transform.DOLocalRotate(Vector3.up, 5f * rotationSpeed * Time.deltaTime).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }
    }

    public void stopRotate()
    {
        rotationModel_1.Kill();
        rotationModel_2.Kill();
        isRotatingLeft = false;
        if (!isRotatingLeft)
        {
            model_1.transform.DOLocalRotate(Vector3.zero, .5f);
            model_2.transform.DOLocalRotate(Vector3.zero, .5f);
        }
        lookAt.y = 180;
        model_1.transform.DOLocalRotate(lookAt,2f).SetEase(Ease.OutElastic);
        model_2.transform.DOLocalRotate(lookAt,2f).SetEase(Ease.OutElastic);
    }

    public void customizePlayer()
    {
        //show customize menu
        Debug.Log("customize");
        mainMenu.transform.DOLocalMoveY(mainMenuTargetPos, menuSpeed).SetEase(Ease.InBack);
        customizeMenu.transform.DOLocalMoveX(0f, menuSpeed).SetEase(Ease.InOutBack);
    }

    public void AnimatePlayer()
    {
        Debug.Log("animate");
        mainMenu.transform.DOLocalMoveY(mainMenuTargetPos, menuSpeed).SetEase(Ease.InBack);
        animateMenu.transform.DOLocalMoveX(0f, menuSpeed).SetEase(Ease.InOutBack);
    }

    public void changeModel()
    {
        Debug.Log("changemodel");
        mainMenu.transform.DOLocalMoveY(mainMenuTargetPos, menuSpeed).SetEase(Ease.InBack);
        changeModelMenu.transform.DOLocalMoveX(0f, menuSpeed).SetEase(Ease.InOutBack);
    }

    public void scalePlayer()
    {
        Debug.Log("scalePlayer");
        //get the current scale
        currentScale = model_1.transform.localScale;
        currentScale = model_2.transform.localScale;

        Vector3 newScale = currentScale + new Vector3(addScale, addScale, addScale); //modelScale = new Vector3(2f, 2f, 2f);
        model_1.transform.DOScale(newScale, .5f).SetEase(Ease.OutElastic);
        model_2.transform.DOScale(newScale, .5f).SetEase(Ease.OutElastic);
    }

    public void resetScale()
    {
        initialScale = new Vector3(7f, 7f, 7f);
        model_1.transform.DOScale(initialScale, .5f).SetEase(Ease.OutElastic);
        model_2.transform.DOScale(initialScale, .5f).SetEase(Ease.OutElastic);
    }

    public void changeColor()
    {
        Debug.Log("changeColor");

        currentMaterialIndexModel_1 = (currentMaterialIndexModel_1 + 1) % model_1_Materials.Length;
        currentMaterialIndexModel_2 = (currentMaterialIndexModel_2 + 1) % model_2_Materials.Length;


        foreach (GameObject part_1 in model_1_Parts)
        {
            Renderer rendererModel1 = part_1.GetComponent<Renderer>();

            if (rendererModel1 != null)
            {
                // Change the material of the model part
                rendererModel1.material = model_1_Materials[currentMaterialIndexModel_1];
            }
        }

        foreach (GameObject part_2 in model_2_Parts)
        {
            Renderer rendererModel2 = part_2.GetComponent<Renderer>();

            if (rendererModel2 != null)
            {
                // Change the material of the model part
                rendererModel2.material = model_2_Materials[currentMaterialIndexModel_2];
            }
        }
    }
    
    public void resetMaterial()
    {
        foreach (GameObject part_1 in model_1_Parts)
        {
            Renderer rendererModel1 = part_1.GetComponent<Renderer>();
            if (rendererModel1 != null)
            {
                rendererModel1.material = initiaModel_1_lMaterial;
            }
        }

        foreach (GameObject part_2 in model_2_Parts)
        {
            Renderer rendererModel2 = part_2.GetComponent<Renderer>();
            if (rendererModel2 != null)
            {
                rendererModel2.material = initiaModel_2_lMaterial;
            }
        }
    }

    public void changePlayerModel_1()
    {
        model_1.SetActive(true);
        model_2.SetActive(false);
    }

    public void changePlayerModel_2()
    {
        model_1.SetActive(false);
        model_2.SetActive(true);
    }

    public void animation_1_Model()
    {
        Debug.Log("animation1");

        // Toggle the animation state
    
        // Check if animation 2 is playing, and stop it if needed
        if (isAnimation_2_playing)
        {
            model_1_animator.SetBool("dance_2", false);
            model_2_animator.SetBool("dance_2", false);
            isAnimation_1_playing = false;
            isAnimation_2_playing = false;
        }
        isAnimation_1_playing = !isAnimation_1_playing;

        // Start or stop animation 1 based on the current state
        model_1_animator.SetBool("dance_1", isAnimation_1_playing);
        model_2_animator.SetBool("dance_1", isAnimation_1_playing);
    }

    public void animation_2_Model()
    {
        Debug.Log("animation2");

        // Toggle the animation state
     

        // Check if animation 1 is playing, and stop it if needed
        if (isAnimation_1_playing)
        {
            model_1_animator.SetBool("dance_1", false);
            model_2_animator.SetBool("dance_1", false);
            isAnimation_1_playing = false;
            isAnimation_2_playing = false;
        }
        isAnimation_2_playing = !isAnimation_2_playing;

        // Start or stop animation 2 based on the current state
        model_1_animator.SetBool("dance_2", isAnimation_2_playing);
        model_2_animator.SetBool("dance_2", isAnimation_2_playing);
    }

    public void stopAnimation()
    {
        isAnimation_1_playing = false;
        isAnimation_2_playing = false;

        model_1_animator.SetBool("dance_2", isAnimation_2_playing);
        model_2_animator.SetBool("dance_2", isAnimation_2_playing);

        model_1_animator.SetBool("dance_1", isAnimation_1_playing);
        model_2_animator.SetBool("dance_1", isAnimation_1_playing);

    }


}
