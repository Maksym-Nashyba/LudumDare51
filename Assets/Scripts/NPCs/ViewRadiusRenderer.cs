using Misc;
using UnityEngine;

namespace NPCs
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class ViewRadiusRenderer : MonoBehaviour
    {
        [SerializeField] private SuspiciousObjectsDetector _detector;
        [SerializeField] private int _segments;
        private MeshFilter _filter;
        private MeshRenderer _renderer;
        private Mesh _mesh;

        private void Awake()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _mesh = new Mesh
            {
                name = "DetectionCircle"
            };
            _filter.mesh = _mesh;
            GenerateFullCircle(_mesh, _segments);
            _detector.Disabled += () =>
            {
                _renderer.enabled = false;
            };
        }

        private void Update()
        {
            float[] distancedToObstacle = GetDistancesToObstacles();
            RepositionVertices(distancedToObstacle);
        }

        private float[] GetDistancesToObstacles()
        {
            float[] distances = new float[_segments];

            for (int i = 0; i < _segments; i++)
            {
                RaycastHit hit = _detector.ShootRayAtAngle((float)i / _segments * 360f);
                if (hit.collider is null)
                {
                    distances[i] = _detector.Radius;
                    continue;
                }
                distances[i] = Mathf.Clamp(hit.distance, 0, _detector.Radius);
            }
            
            return distances;
        }

        private void RepositionVertices(float[] distancedToObstacle)
        {
            Vector3[] vertices = _mesh.vertices;
            for (int i = 0; i < _segments; i++)
            {
                vertices[i+1] = vertices[i+1].normalized * (Mathf.Clamp01(distancedToObstacle[i]/_detector.Radius) * _detector.Radius);
            }
            _mesh.SetVertices(vertices);
            
            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z) / _detector.Radius / 4f * _detector.Radius + Vector2.one/2f;
            }
            _mesh.SetUVs(0, uvs);
            _mesh.MarkModified();
        }
        
        private void GenerateFullCircle(Mesh mesh, int segments)
        {
            Vector3[] vertices = new Vector3[segments + 1];
            vertices[0] = Vector3.zero;
            vertices[1] = new Vector3(0, 0, _detector.Radius);
            for (int i = 2; i < vertices.Length; i++)
            {
                float angle = (float) i / segments * 360f;
                Vector2 rotated = Vector2.up.RotatedBy(angle) * _detector.Radius;
                vertices[i] = new Vector3(rotated.x, 0, rotated.y);
            }
            int[] triangles = new int[3 * segments];
            for (int i = 0; i < segments-1; i++)
            {
                triangles[i*3 + 1] = 0;
                triangles[i*3 + 2] = i + 2;
                triangles[i*3] = i + 1;
            }

            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z)/2f + Vector2.one/2f;
            }
            
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.MarkDynamic();
        }
    }
}