mergeInto(LibraryManager.library,
{		
	GetUserName: function()
	{
		let tg = window.Telegram.WebApp;
		
		var userName = '';
		userName = tg.initDataUnsafe.user.username;
		
		// Object-method-args
		myGameInstance.SendMessage('JSJob', 'LoadUserName', userName);
	},
	
	GetUserId: function()
	{
		let tg = window.Telegram.WebApp;
		
		var userId = '';
		userId = tg.initDataUnsafe.user.id;
			
		myGameInstance.SendMessage('JSJob', 'LoadUserId', userId);
	}
	
	GetUserRef: function()
	{
		var refId = '';
		
		var ref_id = 'ref_id';
		if (ref_id=(new RegExp('[?&]'+encodeURIComponent(ref_id)+'=([^&]*)')).exec(location.search))
			refId = decodeURIComponent(ref_id[1]); 
      
        myGameInstance.SendMessage('JSJob', 'LoadUserRefId', refId);
    }
});