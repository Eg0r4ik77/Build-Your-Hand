using Assets.Features.Auth;
using Cysharp.Threading.Tasks;
using ModestTree;
using Newtonsoft.Json;
using System;
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

    private Tab _currentTab;

    // список объектов UserView
    private Dictionary<Tab, Func<UniTask<List<User>>>> _tabsActions;


    private void Start()
    {
        _tabsActions = new()
        {
            {_allUsersTab, GetAllUsers},
            {_followingsTab, GetFollowings},
            {_followersTab, GetFollowers}
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

        List<User> users = await _tabsActions[tab]();

        ClearContent();
        ShowUsers(users);
    }

    private void ClearContent()
    {
        // очистить вьюшки
    }

    private async void SearchForUser(string template)
    {
        List<User> users = await GetUserByTemplate(template);
        ShowUsers(users);
    }

    private void ShowUsers(List<User> users)
    {
        // генерация вьюшек, добавление в список
    }

    private async UniTask<List<User>> GetUserByTemplate(string template)
    {
        return template.IsEmpty()
            ? await _tabsActions[_currentTab]()
            : await GetUsersByRequest($"http://localhost:8088/users/{template}");
    }

    private async UniTask<List<User>> GetAllUsers()
        => await GetUsersByRequest("http://localhost:8088/users");

    private async UniTask<List<User>> GetFollowings()
        => await GetUsersByRequest("http://localhost:8088/1/followings");

    private async UniTask<List<User>> GetFollowers()
        => await GetUsersByRequest("http://localhost:8088/users");

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
