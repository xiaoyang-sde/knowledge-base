# Wireless and Mobile Network

In a wireless network, the base stateion, such as a cell tower or an access ponit, is responsible for sending and receiving data to and from a wireless host that is associated with that base station.

Each wireless host connects to a base station through a wireless communication link. Different wireless link technologies have different transmission rates and could transmit over different distances.

## 802.11 Wireless LAN

The fundamental building block of the 802.11 architecture is the basic service set. Each BSS contains one or more wireless stations and a central base station, known as an access point. Each 802.11 wireless station has a 6-byte MAC address. Each AP has a MAC address for its wireless interface.

In 802.11, each wireless station needs to associate with an AP before it can send or receive network-layer data. The network administrator assigns a Service Set Identifier (SSID) and a channel number to the access point.

To gain Internet access, the wireless device needs to associate with an AP. Each AP sends beacon frames that includes the AP's SSID and MAC address. The wireless device could scan the 11 channels to seek beacon frames or broadcast a probe frame. After selecting the AP with which to associate, the wireless device sends an association request frame to the AP, and the AP responds with an association response frame.

## Multiple Access Protocol

Multiple wireless devices might want to transmit data frames at the same time over the same channel. 802.11 selects the CSMA/CA (carrier sense multiple access with collision avoidance) protocol.

1. If the station has a frame to transmit, it senses the channel. If the channel is idle, it transmits its frame after the Distributed Inter-frame Space (DIFS).
2. If the channel is not idle, it chooses a random backoff value with exponential backoff and starts the timer.
3. When the counter reaches zero and the channel is idle, the station transmits the entire frame and waits for an acknowledgement.
4. If an acknowledgment is received, the transmitting station knows that its frame has been correctly received at the destination station. If there's no acknowledgement, it enters the backoff phase.

802.11 MAC protocol uses link-layer acknowledgements. When the destination station receives a frame that passes the CRC, it waits a short period of time known as the Short Inter-frame Spacing (SIFS) and then sends back an acknowledgment frame. If the transmitting station does not receive an acknowledgment within a given amount of time, it assumes that an error has occurred and retransmits the frame. If an acknowledgment is not received after some fixed number of retransmissions, the transmitting station gives up and discards the frame.

The 802.11 protocol allows a station to use a short Request to Send control frame and a short Clear to Send control frame to reserve access to the channel. When a sender wants to send a DATA frame, it will send an RTS frame to the AP, indicating the total time required to transmit the data frame and the acknowledgment (ACK) frame. When the AP receives the RTS frame, it broadcasts a CTS frame, which gives the sender explicit permission to send and instructs the other stations not to send for the reserved duration. Once the RTS and CTS frames are transmitted, the data and acknowledgement frames should be transmitted without collisions.

## Indirect Routing

Each mobile device has a permanent address and a care-of-address, which is obtained from the DHCP server of the visited network. The correspondent addresses the datagram to the mobile device's permanent address and sends the datagram into the network. The datagram is routed to the mobile device's home network.

The Home Subscriber Service is responsible for interacting with visited networks to track the mobile device's location. If the mobile device is in a visited network, home network's router intercepts the datagram, consults with the HSS to determine the visited network, and forwards the datagram to the visited network where the mobile device is resident. If NAT translation is used, the visited network's router performs NAT translation.

- The correspondent sends the datagram to the mobile.
  - IP Header Source: IP address of the correspondent
  - IP Header Destination: Permanent IP address of the mobile
- The home network's router sends the datagram to the mobile.
  - Outer IP Header Source: Permanent IP address of the mobile
  - Outer IP Header Destination: Care-of IP address of the mobile
  - Inner IP Header Source: IP address of the correspondent
  - Inner IP Header Destination: Permanent IP address of the mobile
- The mobile sends the datagram to the correspondent.
  - Outer IP Header Source: Care-of IP address of the mobile
  - Outer IP Header Destination: IP address of the correspondent
  - Inner IP Header Source: Permanent IP address of the mobile
  - Inner IP Header Destination: IP address of the correspondent

## Direct Routing

The correspondent queries the HSS in the mobile device's home network to discover the visited network in which the mobile is resident. The correspondent establishes an IP tunnel with the visited network's router.
