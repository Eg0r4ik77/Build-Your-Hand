using Assets.Features;
using Assets.Features.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UsersPanel : Panel
{
    [SerializeField] private TMP_InputField _searchInputField;

    [SerializeField] private Tab _allUsersTab;
    [SerializeField] private Tab _followingsTab;
    [SerializeField] private Tab _followersTab;

    private Tab _currentTab;

    // список объектов UserView
    private Dictionary<Tab, Func<List<User>>> _tabsActions;

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

    private void SwitchContent(Tab tab)
    {
        _currentTab?.SetDefaultColor();
        _currentTab = tab;
        _currentTab.Select();

        List<User> users = _tabsActions[tab]();

        ClearContent();
        ShowUsers(users);
    }

    private void SearchForUser(string template)
    {
        Debug.Log("Запрос на поиск пользователя");
    }

    private List<User> GetAllUsers()
    {
        Debug.Log("Запрос на получение всех пользователей");
        return null;
    }

    private List<User> GetFollowings()
    {
        Debug.Log("Запрос на получение подписок");
        return null;
    }

    private List<User> GetFollowers()
    {
        Debug.Log("Запрос на получение подписчиков");
        return null;
    }

    private void ClearContent()
    {
        // очистить вьюшки
    }

    private void ShowUsers(List<User> users)
    {
        // генерация вьюшек, добавление в список
    }
}
