using Assets.Features.Auth;
using Cysharp.Threading.Tasks;
using ModestTree;
using Newtonsoft.Json;
using System.Collections.Generic;
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

    private List<UserView> _userViews = new();

    private Tab _currentTab;
    private Dictionary<Tab, string> _tabsRequests;

    private void Start()
    {
        _tabsRequests = new()
        {
            {_allUsersTab, "http://localhost:8088/users"},
            {_followingsTab, "http://localhost:8088/1/followings"},
            {_followersTab, "http://localhost:8088/users"}
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

        ClearContent();
    }

    private async void SwitchContent(Tab tab)
    {
        _currentTab?.SetDefaultColor();
        _currentTab = tab;
        _currentTab.Select();

        string request = _tabsRequests[tab];
        List<User> users = await GetUsersByRequest(request);

        ClearContent();
        ShowUsers(users);
    }

    private async void SearchForUser(string template)
    {
        ClearContent();
        List<User> users = await GetUserByTemplate(template);
        ShowUsers(users);
    }

    private void ShowUsers(List<User> users)
    {
        foreach (User user in users)
        {
            UserView view = Instantiate(_userViewPrefab, _contentRectTransform);
            view.Initialize(user, _userInfoPanel);
            _userViews.Add(view);
        }
    }

    private void ClearContent()
    {
        _userViews.ForEach(userView => Destroy(userView.gameObject));
        _userViews = new List<UserView>();
    }

    private async UniTask<List<User>> GetUserByTemplate(string template)
    {
        string request = template.IsEmpty()
            ? _tabsRequests[_currentTab]
            : $"http://localhost:8088/users/{template}";

        return await GetUsersByRequest(request);
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