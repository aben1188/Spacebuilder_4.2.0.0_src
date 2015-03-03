//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using Tunynet;
using System.Linq;
using System.Collections.Generic;
using System;
using Tunynet.Common;
using Spacebuilder.Common;
using SpecialTopic.Topic;

namespace Spacebuilder.Bar
{
    public class TopicUrlGetter : IBarUrlGetter
    {
        TopicService topicService = new TopicService();

        /// <summary>
        /// �⻧����id
        /// </summary>
        public string TenantTypeId
        {
            get { return TenantTypeIds.Instance().Topic(); }
        }

        /// <summary>
        /// ��̬ӵ��������
        /// </summary>
        public int ActivityOwnerType
        {
            get { return ActivityOwnerTypes.Instance().Topic(); }
        }

        /// <summary>
        /// �Ƿ�Ϊ˽��״̬
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public bool IsPrivate(long sectionId)
        {
            TopicEntity group = topicService.Get(sectionId);
            if (group == null)
                return false;
            return !group.IsPublic;
        }

        public string ThreadDetail(long threadId, bool onlyLandlord = false, SortBy_BarPost sortBy = SortBy_BarPost.DateCreated, int pageIndex = 1, long? anchorPostId = null, bool isAnchorPostList = false, long? childPostIndex = null)
        {
            BarThread thread = new BarThreadService().Get(threadId);
            if (thread == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(thread.SectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicThreadDetail(spaceKey, threadId, onlyLandlord, sortBy, pageIndex, anchorPostId, isAnchorPostList, childPostIndex);
        }

        /// <summary>
        /// ������ϸ��ʾҳ��
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public string SectionDetail(long sectionId, SortBy_BarThread? sortBy = null, bool? isEssential = null, long? categoryId = null)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicSectionDetail(spaceKey, categoryId, isEssential, sortBy);
        }

        /// <summary>
        /// �༭ҳ��
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public string Edit(long sectionId, long? threadId = null)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicThreadEdit(spaceKey, threadId);
        }

        /// <summary>
        /// �༭����ҳ��
        /// </summary>
        /// <param name="threadId">����id</param>
        /// <param name="postId">����id</param>
        /// <returns>�༭����ҳ��</returns>
        public string EditPost(long threadId, long? postId = null)
        {
            BarThread thread = new BarThreadService().Get(threadId);
            if (thread == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(thread.SectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicEditPost(spaceKey, threadId, postId);
        }

        /// <summary>
        /// �û�����ҳ��
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="sectionId">����id</param>
        /// <returns>�û�����ҳ��</returns>
        public string UserThreads(long userId, long? sectionId = null)
        {
            if (sectionId == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId.Value);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicUserThreads(spaceKey);
        }

        /// <summary>
        /// �û�����ҳ��
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="sectionId">����id</param>
        /// <returns>�û�����ҳ��</returns>
        public string UserPosts(long userId, long? sectionId = null)
        {
            if (sectionId == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId.Value);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicUserThreads(spaceKey, true);
        }

        /// <summary>
        /// ǰ̨��������ҳ��
        /// </summary>
        /// <param name="sectionId">����id</param>
        /// <returns>ǰ̨��������ҳ��</returns>
        public string ManageThreads(long sectionId)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicManageThreads(spaceKey);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sectionId">����id</param>
        /// <returns>�������</returns>
        public string ManagePosts(long sectionId)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicManagePosts(spaceKey);
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sectionId">����id</param>
        /// <returns>�������</returns>
        public string ManageCategories(long sectionId)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicManageThreadCategories(spaceKey);
        }

        /// <summary>
        /// ��ǩ�µ�����
        /// </summary>
        /// <param name="tagName">��ǩ��</param>
        /// <param name="sectionId">����id</param>
        /// <returns>��ǩ�µ�����</returns>
        public string ListByTag(string tagName, long? sectionId = null, SortBy_BarThread? sortBy = null, bool? isEssential = null)
        {
            if (sectionId == null)
                return string.Empty;

            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId.Value);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;

            return SiteUrls.Instance().TopicThreadListByTag(spaceKey, tagName, sortBy, isEssential);
        }

        /// <summary>
        /// ��̨������ҳ
        /// </summary>
        /// <returns></returns>
        public string BackstageHome()
        {
            return SiteUrls.Instance().ManageTopics();
        }


        /// <summary>
        /// ��̨����
        /// </summary>
        /// <returns></returns>
        public string _ManageSubMenu()
        {
            return "~/Applications/Topic/Views/ControlPanelTopic/_ManageTopicSideMenuShortcut.cshtml";
        }

        /// <summary>
        /// �û���ҳ
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <param name="sectionId">����id</param>
        /// <returns></returns>
        public string UserSpaceHome(long userId, long? sectionId = null)
        {
            return SiteUrls.Instance().SpaceHome(userId);
        }
    }
}