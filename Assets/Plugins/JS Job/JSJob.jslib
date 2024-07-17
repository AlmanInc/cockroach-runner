mergeInto(LibraryManager.library,
{		
	GetUserName: function()
	{
		let tg = window.Telegram.WebApp;
		
		var userName = '';
		//userName = tg.initDataUnsafe.user.username;
		userName = `${tg.initDataUnsafe.user.first_name} ${tg.initDataUnsafe.user.last_name}`;
		userName = userName.toString();
		
		// Object-method-args
		myGameInstance.SendMessage('JSJob', 'LoadUserName', userName);
	},
	
	GetUserId: function()
	{
		let tg = window.Telegram.WebApp;
		
		var userId = '';
		userId = tg.initDataUnsafe.user.id;		
		userId = userId.toString();
		
		myGameInstance.SendMessage('JSJob', 'LoadUserId', userId);
	},
	
	GetUserRef: function()
	{
		var refId = '';
		
		//var ref_id = 'ref_id';
		var ref_id = 'tgWebAppStartParam';
		
		if (ref_id=(new RegExp('[?&]'+encodeURIComponent(ref_id)+'=([^&]*)')).exec(location.search))
			refId = decodeURIComponent(ref_id[1]); 
		
		refId = refId.toString();
        myGameInstance.SendMessage('JSJob', 'LoadUserRefId', refId);
    },
	
	ShowMessage: function(str)
	{
		window.alert(UTF8ToString(str));
    },
	
	CopyReferalLink: function(str)
	{
		navigator.clipboard.writeText(UTF8ToString(str));
    }
});