mergeInto(LibraryManager.library,
{		
	GetUserName: function()
	{
		var name = 'name';
		var userName = '';
		if(name=(new RegExp('[?&]'+encodeURIComponent(name)+'=([^&]*)')).exec(location.search))
			userName = decodeURIComponent(name[1]);
		
		// Object-method-args
		myGameInstance.SendMessage('JSJob', 'LoadUserName', userName);
	},
	
	GetUserId: function()
	{
		var id = 'id';
		var userId = '';
		if(id=(new RegExp('[?&]'+encodeURIComponent(id)+'=([^&]*)')).exec(location.search))
			userId = decodeURIComponent(id[1]); 
			
		myGameInstance.SendMessage('JSJob', 'LoadUserId', userId);
	}
});