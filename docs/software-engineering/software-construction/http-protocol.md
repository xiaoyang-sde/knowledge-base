# HTTP Protocol

## Invention of the World Wide Web

- A textual format to represent hypertext documents, the HyperText Markup Language (HTML).
- A simple protocol to exchange these documents, the HypertText Transfer Protocol (HTTP).
- A client to display and edit these documents, the first web browser called the WorldWideWeb.
- A server to give access to the document, an early version of httpd.

## HTTP/0.9

The request is consisted of a single line and started with the `GET` method followed by the path to the resource. The response is consisted of the content of the HTML file.

## HTTP/1.0

- Versioning information was sent within each request.
- A status code line was also sent at the beginning of a response.
- The concept of HTTP headers was introduced for both requests and responses. Metadata could be transmitted and the protocol became extremely flexible and extensible.
- Documents other than plain HTML files could be transmitted.

## HTTP/1.1

HTTP/1.1 clarified ambiguities and introduced numerous improvements to the HTTP protocol.

- A connection could be reused, which saved time. It no longer needed to be opened multiple times to display the resources embedded in the single original document.
- Pipelining was added. This allowed a second request to be sent before the answer to the first one was fully transmitted. This lowered the latency of the communication.
- Chunked responses were also supported and additional cache control mechanisms were introduced.
- Content negotiation, including language, encoding, and type, was introduced. A client and a server could now agree on which content to exchange.
- The ability to host different domains from the same IP address allowed server collocation with the `Host` header.

## HTTP/2

- Multiplexing: Parallel requests can be made over the same connection, removing the constraints of the HTTP/1.x protocol.
- Header Compression: The protocol removes the duplication and overhead of data transmitted.
- Server Push: It allows a server to populate data in a client cache through a mechanism called the server push.

## HTTP/3

The protocl runs on QUIC, a new transport protocol that relies on UDP instead of TCP.

- Faster connection establishment: QUIC allows TLS version negotiation to happen at the same time as the cryptographic and transport handshakes
- Zero round-trip time (0-RTT): For servers they have already connected to, clients can skip the handshake requirement (the process of acknowledging and verifying each other to determine how they will communicate)
- More comprehensive encryption: QUIC's new approach to handshakes will provide encryption by default — a huge upgrade from HTTP/2 — and will help mitigate the risk of attacks

### TLS (Transport Layer Security)

Transport Layer Security, or TLS, is a widely adopted security protocol designed to facilitate privacy and data security for communications over the Internet. The primary use case of TLS is encrypting the communication between web applications and servers, such as web browsers loading a website.

- Encryption: TLS hides the data being transferred from third parties.
- Authentication: TLS ensures that the parties exchanging information are who they claim to be.
- Integrity: TLS verifies that the data has not been forged or tampered with.
