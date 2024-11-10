name: Bug Report
description: File a bug report
labels: ["type: bug"]
body:
  - type: markdown
    attributes:
      value: |
        ## READ AND FOLLOW THE DESCRIPTIONS OF THIS ISSUE TEMPLATE!
        Not doing so may result in your issue being closed without warning. Take your time and properly fill out everything as requested.
  - type: checkboxes
    id: searched
    attributes:
      label: Terms
      options:
        - label: "I'm using the very latest version of ItemsAdder and its dependencies."
          required: true
        - label: "I am sure this is a bug and it is not caused by a misconfiguration or by another plugin."
          required: true
        - label: "I've looked for already existing issues on the [Issue Tracker](https://github.com/PluginBugs/Issues-ItemsAdder/issues) and haven't found any."
          required: true
        - label: "I already searched on the [plugin wiki](https://itemsadder.devs.beer/) to know if a solution is already known."
          required: true
        - label: "I already searched on the [forums](https://forum.devs.beer/) to check if anyone already has a solution for this."
          required: true
        - label: "I tested this with just ItemsAdder and its dependencies and with a vanilla client of the same version as the Server."
          required: true
  - type: input
    id: discord_tag
    attributes:
      label: Discord Username (optional)
      description: How can we get in touch with you if we need more info?
      placeholder: ex. discorduser1234
    validations:
      required: false
  - type: textarea
    id: what-happened
    attributes:
      label: What happened?
      description: A clear and concise description of what the bug is.
      placeholder: "Example: I can't interact with custom furniture if I'm in my own claim"
    validations:
      required: true
  - type: textarea
    id: steps
    attributes:
      label: Steps to reproduce the issue
      description: Describe how to reproduce the issue step by step
      placeholder: |
        1. Go to '...'
        2. Click on '....'
        3. Scroll down to '....'
        4. See error
    validations:
      required: true
  - type: textarea
    id: server_version
    attributes:
      label: Server version
      description: "run `/version` command and paste it here"
      placeholder: |
        Run `/version` command and paste it here
        
        Example:
        This server is running Paper version git-Paper-788 (MC: 1.16.5) (Implementing API version 1.16.5-R0.1-SNAPSHOT)"
    validations:
      required: true
  - type: textarea
    id: plugin_version
    attributes:
      label: ItemsAdder Version
      description: "Execute `/version ItemsAdder` and paste the output here."
      placeholder: |
        Run `/version ItemsAdder` in your server and paste the output here.
        DO NOT PUT "LATEST". DOING SO WILL GET YOUR ISSUE CLOSED!
        
        Example:
        ItemsAdder version 2.4.17
    validations:
      required: true
  - type: textarea
    id: protocollib_version
    attributes:
      label: ProtocolLib Version
      description: "Execute `/version ProtocolLib` and paste the output here."
      placeholder: |
        Run `/version ProtocolLib` in your server and paste the output here.
        DO NOT PUT "LATEST". DOING SO WILL GET YOUR ISSUE CLOSED!
        
        Example:
        ProtocolLib version 2.4.17
    validations:
      required: true
  - type: textarea
    id: lonelibs_version
    attributes:
      label: LoneLibs Version
      description: "Execute `/version LoneLibs` and paste the output here."
      placeholder: |
        Run `/version LoneLibs` in your server and paste the output here.
        DO NOT PUT "LATEST". DOING SO WILL GET YOUR ISSUE CLOSED!
        
        Example:
        LoneLibs version 2.4.17
    validations:
      required: true
  - type: input
    id: full_server_log
    attributes:
      label: Full Server Log
      description: |-
        Upload your latest.log file to [mclo.gs](https://mclo.gs) and share the generated URL here.
        Sensitive info such as IPs will automatically be censored for your privacy.
      placeholder: "https://mclo.gs/..."  
    validations:
      required: true
  - type: textarea
    id: errors
    attributes:
      label: Error (optional)
      description: "Paste any errors you have here. Text pasted here will be formatted as code."
      render: shell
      placeholder: |
        [19:26:04] [Craft Scheduler Thread - 18 - ItemsAdder/WARN]: [ItemsAdder] Plugin ItemsAdder v2.4.16 generated an exception while executing task 22801
          java.lang.ExceptionInInitializerError: null
            at dev.lone.itemsadder.l.j.a(SourceFile:166) ~[ItemsAdder.jar:?]
            at dev.lone.itemsadder.l.j.av(SourceFile:104) ~[ItemsAdder.jar:?]
            at dev.lone.itemsadder.l.a.a(SourceFile:164) ~[ItemsAdder.jar:?]
            at org.bukkit.craftbukkit.v1_17_R1.scheduler.CraftTask.run(CraftTask.java:101) ~[patched_1.17.1.jar:git-Airplane-84]
            at org.bukkit.craftbukkit.v1_17_R1.scheduler.CraftAsyncTask.run(CraftAsyncTask.java:57) [patched_1.17.1.jar:git-Airplane-84]
            at com.destroystokyo.paper.ServerSchedulerReportingWrapper.run(ServerSchedulerReportingWrapper.java:22) [patched_1.17.1.jar:git-Airplane-84]
            at java.util.concurrent.ThreadPoolExecutor.runWorker(ThreadPoolExecutor.java:1130) [?:?]
            at java.util.concurrent.ThreadPoolExecutor$Worker.run(ThreadPoolExecutor.java:630) [?:?]
            at java.lang.Thread.run(Thread.java:831) [?:?]
          Caused by: java.lang.reflect.InaccessibleObjectException: Unable to make field long java.util.zip.ZipEntry.size accessible: module java.base does not "opens java.util.zip" to unnamed module @6f6969ca
            at java.lang.reflect.AccessibleObject.checkCanSetAccessible(AccessibleObject.java:357) ~[?:?]
            at java.lang.reflect.AccessibleObject.checkCanSetAccessible(AccessibleObject.java:297) ~[?:?]
            at java.lang.reflect.Field.checkCanSetAccessible(Field.java:177) ~[?:?]
            at java.lang.reflect.Field.setAccessible(Field.java:171) ~[?:?]
            at com.comphenix.protocol.reflect.FieldUtils.getField(FieldUtils.java:111) ~[PROTOCOLLIB.jar:?]
            at dev.lone.itemsadder.l.k.<clinit>(SourceFile:12) ~[ItemsAdder.jar:?]
            ... 9 more
  - type: textarea
    id: configuration_item
    attributes:
      label: Problematic items yml configuration file (optional)
      description: "Copy and paste the content of any YAML file from ItemsAdder that causes issues. Code pasted here will automatically be formatted as YAML."
      render: yaml
      placeholder: | 
        info:
          namespace: itemsadder
        items:
          ruby_block:
            display_name: display-name-ruby_block
            permission: ruby_block
            resource:
              material: PAPER
              generate: true
              textures:
              - block/ruby_block.png
            specific_properties:
              block:
                placed_model:
                  type: REAL_NOTE
                  break_particles_material: REDSTONE_BLOCK
                break_tools_whitelist:
                - PICKAXE
                - pickaxe
          crystal_block:
            display_name: display-name-crystal_block
            permission: crystal_block
            resource:
              material: PAPER
              generate: true
              textures:
              - block/crystal_block.png
            specific_properties:
              block:
                placed_model:
                  type: REAL_NOTE
                  break_particles_material: PRISMARINE_BRICKS
                break_tools_whitelist:
                - PICKAXE
                - pickaxe
  - type: textarea
    id: configuration_files
    attributes:
      label: Other files, you can drag and drop them here to upload. (optional)
      description: "Drag and drop your files here."
  - type: textarea
    id: screenshots
    attributes:
      label: Screenshots/Videos (you can drag and drop files or paste links)
      description: "If applicable, add screenshots to help explain your problem (you can drag and drop files or paste links)."
  - type: markdown
    attributes:
      value: |
        ## Thanks for taking the time to fill out this bug report!
