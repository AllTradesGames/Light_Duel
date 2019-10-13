using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[][\"Vector3\", \"Quaternion\", \"int\"][\"Vector3\", \"Quaternion\", \"int\"][][][]]")]
	[GeneratedRPCVariableNames("{\"types\":[[][\"position\", \"rotation\", \"team\"][\"position\", \"rotation\", \"team\"][][][]]")]
	public abstract partial class MovementHeadBehavior : NetworkBehavior
	{
		public const byte RPC_READY = 0 + 5;
		public const byte RPC_SPAWN_STAB = 1 + 5;
		public const byte RPC_SPAWN_SLASH = 2 + 5;
		public const byte RPC_PASS_TURN = 3 + 5;
		public const byte RPC_TAKE_DAMAGE = 4 + 5;
		public const byte RPC_YOU_DIED = 5 + 5;
		
		public MovementHeadNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (MovementHeadNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("Ready", Ready);
			networkObject.RegisterRpc("SpawnStab", SpawnStab, typeof(Vector3), typeof(Quaternion), typeof(int));
			networkObject.RegisterRpc("SpawnSlash", SpawnSlash, typeof(Vector3), typeof(Quaternion), typeof(int));
			networkObject.RegisterRpc("PassTurn", PassTurn);
			networkObject.RegisterRpc("TakeDamage", TakeDamage);
			networkObject.RegisterRpc("YouDied", YouDied);

			networkObject.onDestroy += DestroyGameObject;

			if (!obj.IsOwner)
			{
				if (!skipAttachIds.ContainsKey(obj.NetworkId)){
					uint newId = obj.NetworkId + 1;
					ProcessOthers(gameObject.transform, ref newId);
				}
				else
					skipAttachIds.Remove(obj.NetworkId);
			}

			if (obj.Metadata != null)
			{
				byte transformFlags = obj.Metadata[0];

				if (transformFlags != 0)
				{
					BMSByte metadataTransform = new BMSByte();
					metadataTransform.Clone(obj.Metadata);
					metadataTransform.MoveStartIndex(1);

					if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() =>
						{
							transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
							transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
						});
					}
					else if ((transformFlags & 0x01) != 0)
					{
						MainThreadManager.Run(() => { transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform); });
					}
					else if ((transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() => { transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform); });
					}
				}
			}

			MainThreadManager.Run(() =>
			{
				NetworkStart();
				networkObject.Networker.FlushCreateActions(networkObject);
			});
		}

		protected override void CompleteRegistration()
		{
			base.CompleteRegistration();
			networkObject.ReleaseCreateBuffer();
		}

		public override void Initialize(NetWorker networker, byte[] metadata = null)
		{
			Initialize(new MovementHeadNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new MovementHeadNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void Ready(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void SpawnStab(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void SpawnSlash(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void PassTurn(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void TakeDamage(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void YouDied(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}