# Networking

Objects
---------
Network
* `void recv (data, id)`
* `delegate void callback  (data,id)`
* `void send (data, id)`
* `void connect ()`
* `void startServer ()`
* `void close ()`

Topology
* `void topomsg(computer,id,Dsend,Drecv)`
* `void encapsulate (ref data, Dsend, id)`
* `void disconnect ()`
* `void recv (computer,id)`
* `delegate void Ddecap (data, id)`
* `delegate void Drev (id)`
* `delegate void Dsend (computer)`

Protocol
* `void connect (url,port)`
* `void listen (url,port)`
* `void disconnect ()`
* `void send (data, id)`
* `void sendmsg (computer)`
* `void recv (id)`
* `delegate void Dencapsulate (ref data,Topology:Ddecap,id)`
* `delegate void Ddecapsulate (computer,id)`
* `delegate void Dtopomsg (computer, id, Topology:Dsend, Topology:Drecv)`

## Network
### Creating the network
`app -> Network:Network(callback)`

## Recving
### Reciving on id
`Protocol:recv(id) d-> Topology:recv(computer, id) d-> Network:recv(data, id) d-> app(data, id)`

### Recving on all ids
`Protocol:recva() d-> Topology:recv(computer, id) d-> Network:recv(data, id) d-> app(data, id)`

### Recived a topology message. ie: null data
`Protocol:recv(id)
 Protocol:recva() d-> Topology:topomsg(computer, id, Dsend, Drecv)`

## Sending
### Sending to id
`Protocol:send(id) d-> Topology:encapsulate(data,callback,id) d-> Protocol:sendmsg(msg)`

Server Client
-------------
## Cleint
### Connecting to a server
`Network:Connect() -> Protocol:Connect(url,port) d-> Topology:topomsg(computer,id,callbacksend,callbackrecv) d-> Protocol:sendmsg(msg)`
                                                                                                            `d-> [Reciving on id]`

## Server
### Starting a server
`Network:startServer() -> Protocol:listen(url,port) t-> [loop] [Reciving a topology message] [/loop]`
                                                   ` -> [loop] [Recving on all ids] [/loop]`
