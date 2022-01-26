if(localStorage.getItem('boss') == 1 || localStorage.getItem('boss') == 2){
    document.getElementById('adminBack').classList.add('adminBack_active');
}else{
    document.getElementById('adminBack').classList.remove('adminBack_active');
}
