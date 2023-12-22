using Assets.Features.Auth;
using Cysharp.Threading.Tasks;
using ModestTree;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UsersPanel : Panel
{
    [SerializeField] private TMP_InputField _searchInputField;

    [SerializeField] private Tab _allUsersTab;
    [SerializeField] private Tab _followingsTab;
    [SerializeField] private Tab _followersTab;

    [SerializeField] private RectTransform _contentRectTransform;
    [SerializeField] private UserView _userViewPrefab;
    [SerializeField] private UserInfoPanel _userInfoPanel;

    private List<User> _users = new();
    private List<UserView> _userViews = new();

    private Tab _currentTab;
    private Dictionary<Tab, string> _tabsRequests;

    private void Start()
    {
        _tabsRequests = new()
        {
            {_allUsersTab, "http://localhost:8088/users"},
            {_followingsTab, "http://localhost:8088/1/followings"},
            {_followersTab, "http://localhost:8088/1/followers"}
        };

        SwitchContent(_allUsersTab);
    }

    protected override void Enable()
    {
        base.Enable();

        _searchInputField.onValueChanged.AddListener(SearchForUser);

        foreach (Tab tab in new List<Tab>{ _allUsersTab,  _followingsTab, _followersTab})
        {
            tab.Clicked += SwitchContent;
        }
    }

    protected override void Disable()
    {
        base.Disable();

        _searchInputField.onValueChanged.RemoveListener(SearchForUser);


        foreach (Tab tab in new List<Tab> { _allUsersTab, _followingsTab, _followersTab })
        {
            tab.Clicked -= SwitchContent;
        }
    }

    private async void SwitchContent(Tab tab)
    {
        _currentTab?.SetDefaultColor();
        _currentTab = tab;
        _currentTab.Select();

        string request = _tabsRequests[tab];

        ClearContent();
        _users = await GetUsersByRequest(request);
        ShowUsers();
    }

    private void SearchForUser(string template)
    {
        if(template.IsEmpty()) 
            return;

        ClearContent();
        _users = GetUserByTemplate(template);
        ShowUsers();
    }

    private void ShowUsers()
    {
        List<User> users = new(_users);

        foreach (User user in users)
        {
            UserView view = Instantiate(_userViewPrefab, _contentRectTransform);
            view.Initialize(user, _userInfoPanel);

            _userViews.Add(view);
            _users.Add(user);
        }
    }

    private void ClearContent()
    {
        _userViews.ForEach(userView => Destroy(userView.gameObject));
        _userViews = new List<UserView>();
        _users = new();
    }

    private List<User> GetUserByTemplate(string template)
    {
        string regex = "(?i).*" + template + ".*";
        List<User> users = new List<User>();

        foreach (User user in _users)
        {
            if (Regex.IsMatch(user.Login, regex))
            {
                users.Add(user);
            }
        }

        return users;
    }

    private async UniTask<List<User>> GetUsersByRequest(string uri)
    {
        UnityWebRequest request = await UnityWebRequest
            .Get(uri)
            .SendWebRequest()
            .WithCancellation(this.GetCancellationTokenOnDestroy());

        string usersJson = request.downloadHandler.text;

        Debug.Log($"{usersJson}");
        List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

        return users;
    }
}