$(document).ready(function () {
    let typingTimer;
    const debounceDelay = 450;

    console.log("✅ Store.js loaded");

    // 🔍 Instant Search for Store (with debounce)
    $(document).on('keyup', '#storeSearch', function () {
        clearTimeout(typingTimer);
        const searchValue = $(this).val();

        typingTimer = setTimeout(function () {
            console.log("🔎 Typing Store Search:", searchValue);
            $.get("/admin/loadlist/store", { page: 1, search: searchValue }, function (html) {
                $('#listArea').html(html);
            });
        }, debounceDelay);
    });

    // 📄 Pagination click for store list
    $(document).on('click', '[data-page]', function (e) {
        e.preventDefault();
        const page = $(this).data('page');
        const search = $('#storeSearch').val();

        if (page && !$(this).parent().hasClass('disabled')) {
            console.log("📄 Page click:", page, "Search:", search);
            $.get("/admin/loadlist/store", { page, search }, function (html) {
                $('#listArea').html(html);
            });
        }
    });

    window.loadStorePage = function (page) {
        const search = document.getElementById("storeSearch")?.value || "";
        $.get(`/admin/loadlist/store?page=${page}&search=${encodeURIComponent(search)}`, function (html) {
            $('#listArea').html(html);
        });
    };

    window.reloadStoreForm = function (id) {
        $.get(`/admin/store/get/${id}`, function (html) {
            $('#formArea').html(html);
        });
    };

    window.reloadStoreTab = function () {
        loadTabContent('Store');
    };
});
