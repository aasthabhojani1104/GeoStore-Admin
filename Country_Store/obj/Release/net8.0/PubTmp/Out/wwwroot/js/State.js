$(document).ready(function () {
    let typingTimer;
    const debounceDelay = 400;

    console.log("✅ State.js loaded");

    // 🔍 Instant Search
    $(document).on('keyup', '#stateSearch', function () {
        clearTimeout(typingTimer);
        const searchValue = $(this).val();

        typingTimer = setTimeout(function () {
            console.log("🔎 Typing State Search:", searchValue);
            $.get("/admin/loadlist/state", { page: 1, search: searchValue }, function (html) {
                $('#listArea').html(html);
            });
        }, debounceDelay);
    });

    // ⏩ Pagination Click
    $(document).on('click', '[data-page]', function (e) {
        e.preventDefault();
        const page = $(this).data('page');
        const search = $('#stateSearch').val();

        if (page && !$(this).parent().hasClass('disabled')) {
            console.log("📄 Page click:", page, "Search:", search);
            $.get("/admin/loadlist/state", { page, search }, function (html) {
                $('#listArea').html(html);
            });
        }
    });

    // 🌐 Global method to reload specific page
    window.loadStatePage = function (page) {
        const search = document.getElementById("stateSearch")?.value || "";
        $.get(`/admin/loadlist/state?page=${page}&search=${encodeURIComponent(search)}`, function (html) {
            $('#listArea').html(html);
        });
    };

    // 🔁 Reload Edit Form
    window.reloadStateForm = function (id) {
        $.get(`/admin/state/get/${id}`, function (html) {
            $('#formArea').html(html);
        });
    };

    // 🔁 Reload Entire State Tab
    window.reloadStateTab = function () {
        loadTabContent('State');
    };
});
