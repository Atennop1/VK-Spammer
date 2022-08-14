import vk_api
import requests
from vk_api.bot_longpoll import VkBotLongPoll, VkBotEventType
from vk_api.utils import get_random_id
from vk_api.exceptions import ApiError

session = requests.Session()
vk_session = vk_api.VkApi(token="TOKEN OF COMMUNITY")

vk = vk_session.get_api()
longpoll = VkBotLongPoll(vk_session, "ID OF COMMUNITY")

for event in longpoll.listen():
    if event.type == VkBotEventType.MESSAGE_NEW and event.object.text:
        commands = event.object.text.split(" ")
        if commands[0] == '.spam':
            try:
                message_count = int(commands[1])
            except ValueError:
                continue

            target_id = event.chat_id
            vk.messages.send(chat_id=target_id, message="Start spam attack!", random_id=get_random_id())

            result_message = ""
            for j in range(len(commands) - 2):
                result_message += " " + commands[j + 2]

            sended_count = 0
            while sended_count != message_count:
                try:
                    vk.messages.send(chat_id=target_id, message=result_message, random_id=get_random_id())
                    sended_count += 1
                except ApiError:
                    continue
