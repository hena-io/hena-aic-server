using System;

namespace Hena
{
	public struct Vector3
	{
		public const float kEpsilon = 1E-05f;

		public float x;

		public float y;

		public float z;

		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize(this);
			}
		}

		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(Vector3.Dot(this, this));
			}
		}

		public float sqrMagnitude
		{
			get
			{
				return Vector3.Dot(this, this);
			}
		}

		
		public static Vector3 back
		{
			get
			{
				return new Vector3(0f, 0f, -1f);
			}
		}

		public static Vector3 down
		{
			get
			{
				return new Vector3(0f, -1f, 0f);
			}
		}

		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		public static Vector3 left
		{
			get
			{
				return new Vector3(-1f, 0f, 0f);
			}
		}

		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}
		
		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}


		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		public void Set(float new_x, float new_y, float new_z)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
		}

		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			return new Vector3(
				Mathf.Clamp(value1.x, min.x, max.x),
				Mathf.Clamp(value1.y, min.y, max.y),
				Mathf.Clamp(value1.z, min.z, max.z));
		}

		public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			result = new Vector3(
				Mathf.Clamp(value1.x, min.x, max.x),
				Mathf.Clamp(value1.y, min.y, max.y),
				Mathf.Clamp(value1.z, min.z, max.z));
		}

		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			Cross(ref vector1, ref vector2, out vector1);
			return vector1;
		}

		public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			result = new Vector3(vector1.y * vector2.z - vector2.y * vector1.z,
								 -(vector1.x * vector2.z - vector2.x * vector1.z),
								 vector1.x * vector2.y - vector2.x * vector1.y);
		}


		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 a = target - current;
			float magnitude = a.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + a / magnitude * maxDistanceDelta;
		}

		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
		}

		public static Vector3 Normalize(Vector3 a)
		{
			float num = Vector3.Magnitude(a);
			if (num > 1E-05f)
			{
				return a / num;
			}
			return Vector3.zero;
		}

		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[]
			{
				this.x,
				this.y,
				this.z,
			});
		}

		public string ToString(string format)
		{
			return string.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format),
			});
		}

		public static float Dot(Vector3 a, Vector3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			return inDirection - 2.0f * Dot(inDirection, inNormal) * inNormal;
		}

		public static Vector3 Project(Vector3 a, Vector3 b)
		{
			return b * Vector3.Dot(a, b) / Vector3.Dot(b, b);
		}

		public static float Distance(Vector3 a, Vector3 b)
		{
			return Vector3.Magnitude(a - b);
		}

		public static float Magnitude(Vector3 a)
		{
			return Mathf.Sqrt(Vector3.Dot(a, a));
		}

		public static float SqrMagnitude(Vector3 a)
		{
			return Vector3.Dot(a, a);
		}

		public float SqrMagnitude()
		{
			return Vector3.Dot(this, this);
		}

		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}

		
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}
	}
}
