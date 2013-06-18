namespace general{

public ServerClient<Ttransfertype> : ATopology<Ttransfertype>{

override public bool connect(Ttransfertype obj){
	if (this.protocol.connect(obj,"placeholder.soap")){
	return true;
	}
//If operation does not finish anywhere else the connection was not completed properly.
return false;
}

override public void dissconnect(){
this.protocol.dissconnect();
}

}
}
