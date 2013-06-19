/** Abstract Application
	+ Used to derive application objects such as servers & clients.
*/

using general;

namespace general{

public interface IApplication{
/** Initialize Application object
	+Do any pre setting up before application starts
	+Should include things not critical to the creation of the object.
*/
public interface bool init();

/** Start application
*/
public interface bool start();
/** Stop application
	+Should return true on successful stop.
	+Should only stop if application is not busy, in this case return false.
*/
public interface bool stop();
}
}
