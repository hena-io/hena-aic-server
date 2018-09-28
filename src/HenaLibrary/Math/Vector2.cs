using System;

namespace Hena
{
	public struct Vector2
	{
		public const float kEpsilon = 1E-05f;

		public float x;

		public float y;

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
				default:
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
			}
		}


		public static Vector2 down
		{
			get
			{
				return new Vector2(0f, -1f);
			}
		}
		public static Vector2 left
		{
			get
			{
				return new Vector2(-1f, 0f);
			}
		}
		public static Vector2 one
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}
		public static Vector2 right
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}
		public static Vector2 up
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}
		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}


		public Vector2 normalized
		{
			get
			{
				return Vector2.Normalize(this);
			}
		}

		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(Vector2.Dot(this, this));
			}
		}

		public float sqrMagnitude
		{
			get
			{
				return Vector2.Dot(this, this);
			}
		}


		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public void Set(float new_x, float new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
		{
			Vector2 a = target - current;
			float magnitude = a.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + a / magnitude * maxDistanceDelta;
		}

		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
		}

		public override bool Equals(object other)
		{
			if (!(other is Vector2))
			{
				return false;
			}
			Vector2 vector = (Vector2)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y);
		}

		public static Vector2 Normalize(Vector2 a)
		{
			float num = Vector2.Magnitude(a);
			if (num > 1E-05f)
			{
				return a / num;
			}
			return Vector2.zero;
		}

		public void Normalize()
		{
			float num = Vector2.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		public override string ToString()
		{
			return string.Format("({0:F1}, {1:F1})", new object[]
			{
				this.x,
				this.y,
			});
		}

		public string ToString(string format)
		{
			return string.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format)
			});
		}

		public static float Dot(Vector2 a, Vector2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
		{
			return inDirection - 2.0f * Dot(inDirection, inNormal) * inNormal;
		}

		public static Vector2 Project(Vector2 a, Vector2 b)
		{
			return b * Vector2.Dot(a, b) / Vector2.Dot(b, b);
		}

		public static float Distance(Vector2 a, Vector2 b)
		{
			return Vector2.Magnitude(a - b);
		}

		public static float Magnitude(Vector2 a)
		{
			return Mathf.Sqrt(Vector2.Dot(a, a));
		}

		public static float SqrMagnitude(Vector2 a)
		{
			return Vector2.Dot(a, a);
		}

		public float SqrMagnitude()
		{
			return Vector2.Dot(this, this);
		}

		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.99999944E-11f;
		}

		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.99999944E-11f;
		}
	}
}
