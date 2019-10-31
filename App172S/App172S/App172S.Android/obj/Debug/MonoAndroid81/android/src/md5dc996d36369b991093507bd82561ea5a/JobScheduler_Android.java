package md5dc996d36369b991093507bd82561ea5a;


public class JobScheduler_Android
	extends md5dc996d36369b991093507bd82561ea5a.MainActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("App172S.Droid.JobScheduler_Android, App172S.Android", JobScheduler_Android.class, __md_methods);
	}


	public JobScheduler_Android ()
	{
		super ();
		if (getClass () == JobScheduler_Android.class)
			mono.android.TypeManager.Activate ("App172S.Droid.JobScheduler_Android, App172S.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
