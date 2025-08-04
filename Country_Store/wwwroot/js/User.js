$(document).ready(function () {
    let typingTimer;
    const debounceDelay = 450;

   

    // 🔍 Instant search (Google-style)
    $(document).on('keyup', '#userSearch', function () {
        clearTimeout(typingTimer);
        const searchValue = $(this).val();

        typingTimer = setTimeout(function () {
            console.log("🔎 Typing User Search:", searchValue);
            $.get("/admin/loadlist/user", { page: 1, search: searchValue }, function (html) {
                $('#listArea').html(html);
            });
        }, debounceDelay);
    });

    // ⏩ Pagination click for user list
    $(document).on('click', '[data-page]', function (e) {
        e.preventDefault();
        const page = $(this).data('page');
        const search = $('#userSearch').val();

        if (page && !$(this).parent().hasClass('disabled')) {
            console.log("📄 Page click:", page, "Search:", search);
            $.get("/admin/loadlist/user", { page, search }, function (html) {
                $('#listArea').html(html);
            });
        }
    });

    // 📥 Reload User form by ID
    window.reloadUserForm = function (id) {
        $.get('/User/GetUserByID', { id }, function (html) {
            $('#formArea').html(html);
        }).fail(() => alert('❌ Failed to load user form.'));
    };

    // 🔁 Reload the entire User tab
    window.reloadUserTab = function () {
        $.get('/User/LoadTab?tab=User', function (html) {
            $('#formArea').html(html);
        });
        $.get('/User/LoadList?tab=User', function (html) {
            $('#listArea').html(html);
        });
    };

    // 🔁 Manually load user list with pagination and search
    window.loadUserPage = function (page) {
        const search = document.getElementById("userSearch")?.value || "";
        $.get(`/admin/loadlist/user?page=${page}&search=${encodeURIComponent(search)}`, function (html) {
            $('#listArea').html(html);
        });
    };
});
