from django.shortcuts import render
from django.http import JsonResponse
from django.core.exceptions import PermissionDenied
from .models import User
import requests
import json


def check_exist(request):
    #http://{end_point}/dal/check_exist?id={tgid}
    id = request.GET.get('id')
    if id == None or id == '':
        return PermissionDenied 
    is_present = User.objects.filter(tgid=id).exists()   
    return JsonResponse({'id':id, 'exist':is_present})

def add_user(request):
    #http://{end_point}/dal/add_user?id={tgid}&name={name}
    id = request.GET.get('id')
    name = request.GET.get('name')
    if id == None or id == '' or name == None or name == '':
        return PermissionDenied
    user = User(tgid = id, name = name)
    user.save()
    return JsonResponse({'status':True})

def update_user(request):    
    #http://{end_point}/dal/update_user?id={tgid}&name={name}
    id = request.GET.get('id')
    name = request.GET.get('name')
    if id == None or id == '' or name == None or name == '':
        return PermissionDenied
    user = User.objects.filter(tgid=id).first()
    user.name = name
    user.save()
    return JsonResponse({'status':True})

def add_referal(request):     
    #http://{end_point}/dal/add_referal?id={tgid}&referal_id={referal_id}
    id = request.GET.get('id')
    referal_id = request.GET.get('referal_id')       
    if id == None or id == '' or referal_id == None or referal_id == '':
        return PermissionDenied
    user = User.objects.filter(tgid=id).first()
    user.referal_id = referal_id
    user.save()
    return JsonResponse({'status':True})

def get_referal(request):
    #http://{end_point}/dal/get_referal?id={tgid}
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