function ActivatePage(linkID) {
    $('#navLinks').find('li').removeClass('active');
    $('#' + linkID).addClass('active');
}