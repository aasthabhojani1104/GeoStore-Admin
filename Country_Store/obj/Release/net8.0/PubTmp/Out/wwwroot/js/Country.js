$(document).ready(function () {

    let typingTimer;
    const debounceDelay = 400;
    let lastSearch = "";

    $(document).on('keyup', '#countrySearch', function () {
        clearTimeout(typingTimer);
        const searchValue = $(this).val();

        if (searchValue === lastSearch) return;

        typingTimer = setTimeout(function () {
            lastSearch = searchValue;
            $.get("/admin/loadlist/country", { page: 1, search: searchValue }, function (html) {
                $('#listArea').html(html);
            });
        }, debounceDelay);
    });

    
    $(document).on('click', '[data-page]', function (e) {
        e.preventDefault();

        const page = $(this).data('page');
        const search = $('#countrySearch').val();

        if (page && !$(this).parent().hasClass('disabled')) {
            $.get("/admin/loadlist/country", { page, search }, function (html) {
                //$('#listArea').html(html);
            });
        }
    });

  
    window.loadCountryPage = function (page) {
       
        const search = document.getElementById("countrySearch")?.value || "";
        $.get(`/admin/loadlist/country?page=${page}&search=${encodeURIComponent(search)}`, function (html) {
          
            $('#listArea').html(html);
        });
    };

    
    window.reloadCountryForm = function (id) {
      
        $.get(`/admin/country/get/${id}`, function (html) {
            $('#formArea').html(html);
        });
    };

    window.reloadCountryTab = function () {
       
        loadTabContent('Country');
    };
});
