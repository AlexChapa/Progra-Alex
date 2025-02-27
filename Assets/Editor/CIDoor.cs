using UnityEditor;
using UnityEngine;

namespace AlexDoor
{
    [CustomEditor(typeof(Door))] // Este script es de custom editor, y modificara los scripts de tipo Door
    [CanEditMultipleObjects]

    public class CIDoor : Editor
    {

        private Door _door; // Esta referencia la vamos a usar para nosotros poder escribir las variables que existen en el script de puerta
        private string descripcion; // Es para poner hasta arriba de el inspector una descripcion de como funciona cada puerta
        private bool foldKeysNames = false; // Es un booleano para hacer foldout a caracteristicas de ciertos objetos
        private SerializedProperty multipleKeys; // Propiedad para acceder a el arreglo de "keys" en el script de Door

        private void OnEnable() // OnEnable se ejecuta cuando se activa un objeto en la escena // Se activa, al tener ese script en el inspector, exclusivamente de el objeto seleccionado
        {
            _door = (Door)target;
            multipleKeys = serializedObject.FindProperty("keys");
        }

        public override void OnInspectorGUI() // Este metodo sobreescribe TODOS los valores de el inspector 
        {
            serializedObject.Update();
            EditorGUILayout.LabelField(descripcion);
            InspectorLines();
            Spaces(2);

            _door.tipoDePuerta = (TipoDePuerta)EditorGUILayout.EnumPopup("Door Type", _door.tipoDePuerta);

            Spaces(2);

            switch (_door.tipoDePuerta)
            {

                case TipoDePuerta.DeLlave:
                    {
                        descripcion = "Esta puerta requiere de una llave para abrirse. Arrastra un objeto de tipo Item al campo";
                        _door.key = EditorGUILayout.ObjectField("Llave Requerida", _door.key, typeof(SOItem2), false) as SOItem2;
                        break;
                    }

                case TipoDePuerta.Evento:
                    {
                        descripcion = "Esta puerta requiere que se cumpla un evento para poderse abrir";
                        _door.eventoActivado = EditorGUILayout.Toggle("Se puede abrir?", _door.eventoActivado);
                        break;
                    }

                case TipoDePuerta.MultiplesLlaves:
                    {
                        descripcion = "Esta puerta requiere de multiples llaves para abrirse. Indica en el arreglo cuantas llaves necesita y cuales";
                        EditorGUILayout.PropertyField(multipleKeys, new GUIContent("Llaves Requeridas"), true);

                        if (_door.showKeysNames == true) // showKeysNames es un bool que se encuentra en el script de Door
                        {
                            foldKeysNames = EditorGUILayout.Foldout(foldKeysNames, "Keys"); // foldKeysNames esta escrito en este script hasta arriba
                            if (foldKeysNames == true)
                            {
                                for (int i = 0; i < _door.keys.Length; i++)
                                {
                                    if (_door.keys[i] != null)
                                    {
                                        EditorGUILayout.LabelField(i + ".- " + _door.keys[i].name);
                                        Texture2D imagen = _door.keys[i].sprite.texture;
                                        EditorGUILayout.LabelField(new GUIContent(imagen), GUILayout.Width(50), GUILayout.Height(50));

                                    }
                                    else
                                    {
                                        EditorGUILayout.LabelField(i + ".- Este espacio esta vacio");
                                    }
                                }
                            }
                        }

                        break;
                    }

                case TipoDePuerta.Automatica:
                    {
                        descripcion = "Esta puerta se abre cuando te acercas";
                        break;
                    }

                case TipoDePuerta.Normal:
                    {
                        descripcion = "Esta puerta se abre manualmente";
                        break;
                    }
            }
            serializedObject.ApplyModifiedProperties();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void Spaces(int spaces)
        {
            for (int i = 0; i < spaces; i++)
            {
                EditorGUILayout.Space();
            }
        }

        private void InspectorLines()
        {
            EditorGUILayout.LabelField(WidthLines());
        }

        string lines;
        private string WidthLines()
        {
            lines = string.Empty;

            for (int i = 0; i < EditorGUIUtility.currentViewWidth; i++)
            {
                lines += "-";
            }

            return lines;
        }

    }
}



