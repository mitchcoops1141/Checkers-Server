using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

enum Checker_Board_Square_State
{
    Empty,
    Red,
    Black
};

public class test_get_checkboard : MonoBehaviour
{
    Dictionary<string, Checker_Board_Square_State> checkerboard = new Dictionary<string, Checker_Board_Square_State>();

    void Start()
    {
        StartCoroutine(Test_Mutation());

    }

    IEnumerator Test_Mutation()
    {
        yield return Get_Checkerboard_Square("a0");
        yield return Set_Checkerboard_Square("a0", Checker_Board_Square_State.Empty);
        yield return Get_Checkerboard_Square("a0");
    }

   IEnumerator Get_Checkerboard_Square(string checkerboard_square)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:9000/get_checkerboard_square/" + checkerboard_square);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var color_of_square = System.Text.Encoding.Default.GetString(www.downloadHandler.data);
            checkerboard[checkerboard_square] = Convert_From_String(color_of_square);

            Debug.Log(checkerboard_square);
            Debug.Log(checkerboard[checkerboard_square]);
        }
    }

    Checker_Board_Square_State Convert_From_String(string checkerboard_square_state)
    {
        if (checkerboard_square_state == "red") return Checker_Board_Square_State.Red;
        if (checkerboard_square_state == "empty") return Checker_Board_Square_State.Empty;
        if (checkerboard_square_state == "black") return Checker_Board_Square_State.Black;

        throw new System.Exception("This is bad, stop being naughty.");
    }

    string Convert_To_String(Checker_Board_Square_State checkerboard_square_state)
    {
        if (checkerboard_square_state == Checker_Board_Square_State.Red) return "red";
        if (checkerboard_square_state == Checker_Board_Square_State.Empty) return "empty";
        if (checkerboard_square_state == Checker_Board_Square_State.Black) return "black";

        throw new System.Exception("This is bad, stop being naughty.");
    }

    IEnumerator Set_Checkerboard_Square(string checkerboard_square, Checker_Board_Square_State state)
    {
        UnityWebRequest www = UnityWebRequest.Post("http://localhost:9000/set_checkerboard_square/" + checkerboard_square + "/" + Convert_To_String(state), "");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
    }
}


/*
 using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MyBehavior : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Upload());
    }

    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", formData);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
*/