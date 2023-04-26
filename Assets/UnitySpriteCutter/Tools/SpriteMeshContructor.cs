using UnityEngine;
using System.Collections.Generic;

namespace UnitySpriteCutter.Tools {
	
	public static class SpriteMeshConstructor {

		public static Mesh ConstructFromRendererBounds( SpriteRenderer renderer,
			int headX, int headY, int headWidth, int headHeight, int textureWidth, int textureHeight)
		{
			Debug.Log(renderer.name);
			Mesh result = new Mesh();

            if (renderer.sprite == null)
            {
                throw new System.Exception("Cannot cut from null sprite!");
            }

			Vector3[] vertices = new Vector3[ 4 ];
			vertices[ 0 ] = new Vector2(0, 1);
			vertices[ 1 ] = new Vector2(1, 1);
			vertices[ 2 ] = new Vector2(0, 0);
			vertices[ 3 ] = new Vector2(1, 0);

			int[] triangles = new int[ 6 ];
			triangles[ 0 ] = 0;
			triangles[ 1 ] = 1;
			triangles[ 2 ] = 2;
			triangles[ 3 ] = 2;
			triangles[ 4 ] = 1;
			triangles[ 5 ] = 3;

			Vector2[] uv = new Vector2[ 4 ];
			uv[ 0 ] = new Vector2( 0, 1 );
			uv[ 1 ] = new Vector2( 1, 1 );
			uv[ 2 ] = new Vector2( 0, 0 );
			uv[ 3 ] = new Vector2( 1, 0 );

			uv[0] = ConvertPixelsToUVCoordinates(headX, headY + headHeight, textureWidth, textureHeight);
			uv[1] = ConvertPixelsToUVCoordinates(headX + headWidth, headY + headHeight, textureWidth, textureHeight);
			uv[2] = ConvertPixelsToUVCoordinates(headX, headY, textureWidth, textureHeight);
			uv[3] = ConvertPixelsToUVCoordinates(headX + headWidth, headY, textureWidth, textureHeight);

			result.vertices = vertices;
			result.triangles = triangles;
			result.uv = uv;
			result.Optimize();
			result.RecalculateNormals();

			return result;
		}

		public static Vector2 ConvertPixelsToUVCoordinates(int x, int y, int textureWidth, int textureHeight)
        {
			return new Vector2((float)x / textureWidth, (float)y / textureHeight);
        }
	}

}