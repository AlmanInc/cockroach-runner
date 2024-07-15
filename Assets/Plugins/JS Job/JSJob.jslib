mergeInto(LibraryManager.library,
{		
	GetUserName: function()
	{
		//var name = 'name';
		//var userName = '';
		//if(name=(new RegExp('[?&]'+encodeURIComponent(name)+'=([^&]*)')).exec(location.search))
		//	userName = decodeURIComponent(name[1]);
		
		let tg = window.Telegram.WebApp;
		
		var userName = '';
		userName = tg.initDataUnsafe.user.username;
		
		// Object-method-args
		myGameInstance.SendMessage('JSJob', 'LoadUserName', userName);
	},
	
	GetUserId: function()
	{
		//var id = 'id';
		//var userId = '';
		//if(id=(new RegExp('[?&]'+encodeURIComponent(id)+'=([^&]*)')).exec(location.search))
		//	userId = decodeURIComponent(id[1]); 
		
		let tg = window.Telegram.WebApp;
		
		var userId = '';
		userId = tg.initDataUnsafe.user.id;
			
		myGameInstance.SendMessage('JSJob', 'LoadUserId', userId);
	}
});