from django.shortcuts import render
from django.http import JsonResponse
from django.core.exceptions import PermissionDenied
from .models import User, Task, User_Task
import requests
import json
import datetime

def commit_task(request):
    id = request.GET.get('id')
    task_id = request.GET.get('task_id')
    if id == None or id == '' or task_id == None or task_id == '':
        return PermissionDenied    
    user = User.objects.get(tgid=id)        
    task = Task.objects.get(id=task_id)
    user_task = User_Task.objects.get(user=user, task=task)
    user_task.done = True
    user_task.done_date = datetime.datetime.now()
    user_task.save()
    return JsonResponse({'status':True})

def get_tasks(request):
    id = request.GET.get('id')    
    if id == None or id == '':
        return PermissionDenied    
    
    user = User.objects.get(tgid=id)            
    js_response = {'tasks':[]}
    for task in User_Task.objects.filter(user=user):
        js_response['tasks'].append({
            'task_id': task.task.id,
            'name': task.task.name,
            'description': task.task.description,
            'cost': task.task.cost,
            'type': task.task.type,
            'params': task.task.params,
            'done': task.done,
            'done_date': task.done_date,
        })        
    return JsonResponse(js_response)        

def check_exist(request):
    id = request.GET.get('id')
    if id == None or id == '':
        return PermissionDenied 
    is_present = User.objects.filter(tgid=id).exists()   
    return JsonResponse({'id':id, 'exist':is_present})

def add_user(request):    
    id = request.GET.get('id')
    name = request.GET.get('name')
    if id == None or id == '' or name == None or name == '':
        return PermissionDenied           

    user = User(tgid=id, name=name)
    user.save()

    tasks = Task.objects.all()
    for task in tasks:
        if not task.user.filter(tgid=id).exists():         
            task.user.add(user)

    return JsonResponse({'status':True})

def update_user(request):        
    id = request.GET.get('id')
    name = request.GET.get('name')
    if id == None or id == '' or name == None or name == '':
        return PermissionDenied
    user = User.objects.filter(tgid=id).first()
    user.name = name
    user.save()
    return JsonResponse({'status':True})

def add_referal(request):         
    id = request.GET.get('id')
    referal_id = request.GET.get('referal_id')       
    if id == None or id == '' or referal_id == None or referal_id == '':
        return PermissionDenied
    
    user = User.objects.filter(tgid=id).first()
    user.referal_id = referal_id
    user.save()

    user_owner_ref = User.objects.filter(tgid=referal_id).first()                
    len_referals = len(User.objects.filter(referal_id=referal_id))  
    if len_referals == 1:
        user_owner_ref.balance += 20
    elif len_referals == 5:
        user_owner_ref.balance += 100
    elif len_referals == 10:
        user_owner_ref.balance += 150
    elif len_referals == 15:
        user_owner_ref.balance += 200
    user_owner_ref.save()

    return JsonResponse({'status':True})

def get_referal(request):    
    id = request.GET.get('id')
    if id == None or id == '':
        return PermissionDenied
    referals = User.objects.filter(referal_id=id).values_list('tgid', 'name', 'referal_id')    
    return JsonResponse({'status':True, 'referals':list(referals)})
    
def get_cur_course(request):
    r = requests.get('https://api.binance.com/api/v3/ticker/price?symbol=BTCUSDT')
    js = json.loads(r.text)
    price = round(float(js['price']), 3)
    return JsonResponse({'status':True, 'price':price})

def get_balance(request):
    id = request.GET.get('id')
    if id == None or id == '':
        return PermissionDenied
    user = User.objects.filter(tgid=id).first()
    return JsonResponse({'status':True, 'balance':user.balance})

def add_balance(request):
    id = request.GET.get('id')
    balance = request.GET.get('balance')       
    if id == None or id == '' or balance == None or balance == '':
        return PermissionDenied
    user = User.objects.filter(tgid=id).first()
    user.balance += int(balance)
    user.save()
    return JsonResponse({'status':True, 'balance':user.balance})