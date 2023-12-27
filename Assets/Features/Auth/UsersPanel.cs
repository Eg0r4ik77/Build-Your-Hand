using Assets.Features.Auth;
using Cysharp.Threading.Tasks;
using ModestTree;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

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
    private Dictionary<Tab, Task<List<User>>> _tabsRequests;

    private UserService _userService;

    [Inject]
    private void Construct(UserService userService)
    {
        _userService = userService;
    }

    private void Start()
    {
        _tabsRequests = new()
        {
            {_allUsersTab, _userService.GetAllUsers()},
            {_followingsTab, _userService.GetFollowings()},
            {_followersTab, _userService.GetFollowers()}
        };

        _userInfoPanel.Initialize(_userService);
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

        ClearContent();
        _users = await _tabsRequests[tab];
        ShowUsers();
    }

    private async void SearchForUser(string template)
    {
        if(template.IsEmpty()) 
            return;

        ClearContent();
        _users = await _tabsRequests[_currentTab];
        _users = GetUsersByTemplate(template);
        ShowUsers();
    }

    private void ShowUsers()
    {
        if (_users == null)
        {
            Debug.Log("No users");
            return;
        }

        List<User> users = new(_users);

        users = users
            .OrderBy(user => user.Id)
            .ToList();
        
        foreach (User user in users)
        {
            UserView view = Instantiate(_userViewPrefab, _contentRectTransform);
            view.Initialize(user, _userService, _userInfoPanel);

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

    private List<User> GetUsersByTemplate(string template)
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
}