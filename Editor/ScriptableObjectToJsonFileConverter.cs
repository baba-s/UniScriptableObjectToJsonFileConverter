using System.IO;
using System.Text;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// 設定を管理するクラス
	/// </summary>
	public static class ScriptableObjectToJsonFileConverter
	{
		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// 設定を ProjectSettings フォルダから読み込みます
		/// </summary>
		public static T Load<T>( string path, T instance ) where T : ScriptableObject
		{
			if ( instance != null ) return instance;

			instance = ScriptableObject.CreateInstance<T>();

			if ( !File.Exists( path ) ) return instance;

			var json = File.ReadAllText( path, Encoding.UTF8 );

			JsonUtility.FromJsonOverwrite( json, instance );

			if ( instance == null )
			{
				instance = ScriptableObject.CreateInstance<T>();
			}

			return instance;
		}

		/// <summary>
		/// 設定を ProjectSettings フォルダに保存します
		/// </summary>
		public static void Save<T>( string path, T instance ) where T : ScriptableObject
		{
			var json = JsonUtility.ToJson( instance, true );

			File.WriteAllText( path, json, Encoding.UTF8 );
		}
	}
}