using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0.15,0.15,0.15,0.15]")]
	public partial class MovementHeadNetworkObject : NetworkObject
	{
		public const int IDENTITY = 8;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _headPosition;
		public event FieldEvent<Vector3> headPositionChanged;
		public InterpolateVector3 headPositionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 headPosition
		{
			get { return _headPosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_headPosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_headPosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetheadPositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_headPosition(ulong timestep)
		{
			if (headPositionChanged != null) headPositionChanged(_headPosition, timestep);
			if (fieldAltered != null) fieldAltered("headPosition", _headPosition, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _headRotation;
		public event FieldEvent<Quaternion> headRotationChanged;
		public InterpolateQuaternion headRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion headRotation
		{
			get { return _headRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_headRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_headRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetheadRotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_headRotation(ulong timestep)
		{
			if (headRotationChanged != null) headRotationChanged(_headRotation, timestep);
			if (fieldAltered != null) fieldAltered("headRotation", _headRotation, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _leftPosition;
		public event FieldEvent<Vector3> leftPositionChanged;
		public InterpolateVector3 leftPositionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 leftPosition
		{
			get { return _leftPosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_leftPosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_leftPosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetleftPositionDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_leftPosition(ulong timestep)
		{
			if (leftPositionChanged != null) leftPositionChanged(_leftPosition, timestep);
			if (fieldAltered != null) fieldAltered("leftPosition", _leftPosition, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _leftRotation;
		public event FieldEvent<Quaternion> leftRotationChanged;
		public InterpolateQuaternion leftRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion leftRotation
		{
			get { return _leftRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_leftRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_leftRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetleftRotationDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_leftRotation(ulong timestep)
		{
			if (leftRotationChanged != null) leftRotationChanged(_leftRotation, timestep);
			if (fieldAltered != null) fieldAltered("leftRotation", _leftRotation, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _rightPosition;
		public event FieldEvent<Vector3> rightPositionChanged;
		public InterpolateVector3 rightPositionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 rightPosition
		{
			get { return _rightPosition; }
			set
			{
				// Don't do anything if the value is the same
				if (_rightPosition == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_rightPosition = value;
				hasDirtyFields = true;
			}
		}

		public void SetrightPositionDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_rightPosition(ulong timestep)
		{
			if (rightPositionChanged != null) rightPositionChanged(_rightPosition, timestep);
			if (fieldAltered != null) fieldAltered("rightPosition", _rightPosition, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _rightRotation;
		public event FieldEvent<Quaternion> rightRotationChanged;
		public InterpolateQuaternion rightRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rightRotation
		{
			get { return _rightRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rightRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_rightRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrightRotationDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_rightRotation(ulong timestep)
		{
			if (rightRotationChanged != null) rightRotationChanged(_rightRotation, timestep);
			if (fieldAltered != null) fieldAltered("rightRotation", _rightRotation, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			headPositionInterpolation.current = headPositionInterpolation.target;
			headRotationInterpolation.current = headRotationInterpolation.target;
			leftPositionInterpolation.current = leftPositionInterpolation.target;
			leftRotationInterpolation.current = leftRotationInterpolation.target;
			rightPositionInterpolation.current = rightPositionInterpolation.target;
			rightRotationInterpolation.current = rightRotationInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _headPosition);
			UnityObjectMapper.Instance.MapBytes(data, _headRotation);
			UnityObjectMapper.Instance.MapBytes(data, _leftPosition);
			UnityObjectMapper.Instance.MapBytes(data, _leftRotation);
			UnityObjectMapper.Instance.MapBytes(data, _rightPosition);
			UnityObjectMapper.Instance.MapBytes(data, _rightRotation);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_headPosition = UnityObjectMapper.Instance.Map<Vector3>(payload);
			headPositionInterpolation.current = _headPosition;
			headPositionInterpolation.target = _headPosition;
			RunChange_headPosition(timestep);
			_headRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			headRotationInterpolation.current = _headRotation;
			headRotationInterpolation.target = _headRotation;
			RunChange_headRotation(timestep);
			_leftPosition = UnityObjectMapper.Instance.Map<Vector3>(payload);
			leftPositionInterpolation.current = _leftPosition;
			leftPositionInterpolation.target = _leftPosition;
			RunChange_leftPosition(timestep);
			_leftRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			leftRotationInterpolation.current = _leftRotation;
			leftRotationInterpolation.target = _leftRotation;
			RunChange_leftRotation(timestep);
			_rightPosition = UnityObjectMapper.Instance.Map<Vector3>(payload);
			rightPositionInterpolation.current = _rightPosition;
			rightPositionInterpolation.target = _rightPosition;
			RunChange_rightPosition(timestep);
			_rightRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rightRotationInterpolation.current = _rightRotation;
			rightRotationInterpolation.target = _rightRotation;
			RunChange_rightRotation(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _headPosition);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _headRotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _leftPosition);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _leftRotation);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rightPosition);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rightRotation);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (headPositionInterpolation.Enabled)
				{
					headPositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					headPositionInterpolation.Timestep = timestep;
				}
				else
				{
					_headPosition = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_headPosition(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (headRotationInterpolation.Enabled)
				{
					headRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					headRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_headRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_headRotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (leftPositionInterpolation.Enabled)
				{
					leftPositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					leftPositionInterpolation.Timestep = timestep;
				}
				else
				{
					_leftPosition = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_leftPosition(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (leftRotationInterpolation.Enabled)
				{
					leftRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					leftRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_leftRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_leftRotation(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (rightPositionInterpolation.Enabled)
				{
					rightPositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					rightPositionInterpolation.Timestep = timestep;
				}
				else
				{
					_rightPosition = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_rightPosition(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (rightRotationInterpolation.Enabled)
				{
					rightRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rightRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rightRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rightRotation(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (headPositionInterpolation.Enabled && !headPositionInterpolation.current.UnityNear(headPositionInterpolation.target, 0.0015f))
			{
				_headPosition = (Vector3)headPositionInterpolation.Interpolate();
				//RunChange_headPosition(headPositionInterpolation.Timestep);
			}
			if (headRotationInterpolation.Enabled && !headRotationInterpolation.current.UnityNear(headRotationInterpolation.target, 0.0015f))
			{
				_headRotation = (Quaternion)headRotationInterpolation.Interpolate();
				//RunChange_headRotation(headRotationInterpolation.Timestep);
			}
			if (leftPositionInterpolation.Enabled && !leftPositionInterpolation.current.UnityNear(leftPositionInterpolation.target, 0.0015f))
			{
				_leftPosition = (Vector3)leftPositionInterpolation.Interpolate();
				//RunChange_leftPosition(leftPositionInterpolation.Timestep);
			}
			if (leftRotationInterpolation.Enabled && !leftRotationInterpolation.current.UnityNear(leftRotationInterpolation.target, 0.0015f))
			{
				_leftRotation = (Quaternion)leftRotationInterpolation.Interpolate();
				//RunChange_leftRotation(leftRotationInterpolation.Timestep);
			}
			if (rightPositionInterpolation.Enabled && !rightPositionInterpolation.current.UnityNear(rightPositionInterpolation.target, 0.0015f))
			{
				_rightPosition = (Vector3)rightPositionInterpolation.Interpolate();
				//RunChange_rightPosition(rightPositionInterpolation.Timestep);
			}
			if (rightRotationInterpolation.Enabled && !rightRotationInterpolation.current.UnityNear(rightRotationInterpolation.target, 0.0015f))
			{
				_rightRotation = (Quaternion)rightRotationInterpolation.Interpolate();
				//RunChange_rightRotation(rightRotationInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public MovementHeadNetworkObject() : base() { Initialize(); }
		public MovementHeadNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public MovementHeadNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
