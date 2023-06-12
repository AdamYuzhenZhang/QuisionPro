using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;

// Netcode-tutorial
// https://github.com/gamedevsatyam/netcode-tutorial/blob/main/

public class NetworkController : MonoBehaviour
{
    //private PlayerController pc;
	private bool pcAssigned;

	[SerializeField] TextMeshProUGUI ipAddressText;
	[SerializeField] TMP_InputField ip;

	[SerializeField] string ipAddress;
	[SerializeField] UnityTransport transport;

	//public string IPAddress => ipAddress;

	void Start()
	{
		//Debug.Log(GetLocalIPAddress());
		ipAddress = "0.0.0.0";
		SetIpAddress(); // Set the Ip to the above address
		pcAssigned = false;
	}

	// To Host a game
	public void StartHost() {
		NetworkManager.Singleton.StartHost();
		GetLocalIPAddress();
	}

	// To Join a game
	public void StartClient() {
		ipAddress = ip.text;
		SetIpAddress();
		NetworkManager.Singleton.StartClient();
	}

	/* Gets the Ip Address of your connected network and
	shows on the screen in order to let other players join
	by inputing that Ip in the input field */
	// ONLY FOR HOST SIDE 
	public string GetLocalIPAddress() {
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList) {
			if (ip.AddressFamily == AddressFamily.InterNetwork && IsWirelessIP(ip)) {
				ipAddressText.text = ip.ToString();
				ipAddress = ip.ToString();
				return ip.ToString();
			}
		}
		throw new System.Exception("No network adapters with an IPv4 address in the system!");
	}
	
	private bool IsWirelessIP(IPAddress ipAddress)
	{
		// Check if the IP address belongs to a wireless interface
		// Modify this method according to your specific requirements
		// You can use additional criteria like the interface name or other properties
    
		// Example: Check if the IP address is within a specific range
		var ipBytes = ipAddress.GetAddressBytes();
		if (ipBytes[0] == 192 && ipBytes[1] == 168 && ipBytes[2] == 1)
		{
			return true;
		}

		return false;
	}

	/* Sets the Ip Address of the Connection Data in Unity Transport
	to the Ip Address which was input in the Input Field */
	// ONLY FOR CLIENT SIDE
	public void SetIpAddress() {
		transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
		transport.ConnectionData.Address = ipAddress;
	}
}
