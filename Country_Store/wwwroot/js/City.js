$(document).ready(function () {
    let typingTimer;
    const debounceDelay = 300;

    console.log("✅ City.js loaded");

   
    $(document).on('keyup', '#citySearch', function () {
        clearTimeout(typingTimer);
        const searchValue = $(this).val();

        typingTimer = setTimeout(function () {
            console.log("🔎 Typing City Search:", searchValue);
            $.get("/admin/loadlist/city", { page: 1, search: searchValue }, function (html) {
                $('#listArea').html(html);
            });
        }, debounceDelay);
    });

    // ⏩ Pagination click
    $(document).on('click', '[data-page]', function (e) {
        e.preventDefault();
        const page = $(this).data('page');
        const search = $('#citySearch').val();

        if (page && !$(this).parent().hasClass('disabled')) {
            console.log("📄 Page click:", page, "Search:", search);
            $.get("/admin/loadlist/city", { page, search }, function (html) {
                $('#listArea').html(html);
            });
        }
    });

    // 🌐 Global function to load specific city page
    window.loadCityPage = function (page) {
        const search = document.getElementById("citySearch")?.value || "";
        $.get(`/admin/loadlist/city?page=${page}&search=${encodeURIComponent(search)}`, function (html) {
            $('#listArea').html(html);
        });
    };

    // ✏️ Load Edit City Form
    window.reloadCityForm = function (id) {
        $.get(`/admin/city/get/${id}`, function (html) {
            $('#formArea').html(html);
        });
    };

    // 🔁 Reload Entire City Tab
    window.reloadCityTab = function () {
        loadTabContent('City');
    };
});
