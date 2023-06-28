using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class BlackBackgroundObjectNameEditor
{
    static BlackBackgroundObjectNameEditor()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (obj != null)
        {
            GUIStyle style = new GUIStyle(EditorStyles.label);
            style.fixedHeight = selectionRect.height;

            string objName = obj.name;

            bool isChildObject = obj.transform.parent != null; // Verificar si es hijo de otro objeto

            if (objName.StartsWith("//"))
            {
                objName = objName.Remove(0, 2); // Eliminar los dos primeros caracteres "//"
                objName = objName.ToUpper(); // Convertir a mayúsculas

                if (isChildObject)
                {
                    EditorGUI.DrawRect(selectionRect, Color.blue); // Color verde para objetos hijos
                }
                else
                {
                    EditorGUI.DrawRect(selectionRect, Color.black); // Color negro para objetos no hijos
                    style.normal.textColor = Color.white; // Cambiar el color del texto a blanco
                }

                EditorGUI.LabelField(selectionRect, objName, style);
            }
        }
    }
}
